using System;
using System.Linq;
using VoiceBridge.Common;
using VoiceBridge.Most.Directives;
using VoiceBridge.Most.Directives.Processors;
using VoiceBridge.Most.Google;
using VoiceBridge.Most.Test.TestData;
using VoiceBridge.Most.VoiceModel.Alexa;
using VoiceBridge.Most.VoiceModel.Alexa.Directives;
using VoiceBridge.Most.VoiceModel.Alexa.Directives.AudioPlayer;
using VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK;
using VoiceBridge.Most.VoiceModel.GoogleAssistant.DialogFlow;
using Xunit;

namespace VoiceBridge.Most.Test.Directives.Processors
{
    public class PlayMediaProcessorTest
    {
        private const string PromptText = "hello";
        
        [Fact]
        public void PlayMediaOnAlexa()
        {
            var request = AlexaRequests.Boilerplate();
            var response = AlexaResponses.Boilerplate();
            var media = ExecuteProcessor((virtualDirective, processor) => processor.Process(virtualDirective, request, response));
            
            var directive = (PlayAudioDirective) response.Content.Directives.FirstOrDefault(d => d is PlayAudioDirective);
            var outputSpeech = (PlainTextOutputSpeech) response.Content.OutputSpeech;
            
            Assert.NotNull(directive.Audio);
            Assert.Equal(AlexaConstants.AudioPlayer.AudioPlayerBehavior.ReplaceAll, directive.PlayBehavior);
            Assert.Equal(media.StreamUrl, directive.Audio.StreamInfo.Stream);
            Assert.Equal(media.Token, directive.Audio.StreamInfo.Token);
            Assert.Null(directive.Audio.StreamInfo.ExpectedPreviousToken);
            Assert.Equal(0, directive.Audio.StreamInfo.OffsetInMilliseconds);
            
            Assert.Equal(media.Title, directive.Audio.Metadata.Title);
            Assert.Equal(media.Subtitle, directive.Audio.Metadata.Subtitle);
            Assert.Equal(media.LargeImageUrl, directive.Audio.Metadata.Art.Sources.Single().Url);
            Assert.Null(directive.Audio.Metadata.BackgroundImages);
            Assert.Equal(PromptText, outputSpeech.Text);
        }
        
        [Fact]
        public void PlayMediaOnGoogleAssistant()
        {
            var request = AppRequests.Boilerplate();
            var response = new ActionResponseFactory().Create(new ConversationContext());
            var media = ExecuteProcessor((virtualDirective, processor) => processor.Process(virtualDirective, request, response));
            
            Assert.Equal(PromptText, GetItem<SimpleResponseItem>(response).Value.TextToSpeech);
            var mediaObject = GetItem<MediaResponseItem>(response).Value;
            Assert.NotNull(mediaObject);
            Assert.Equal(GoogleAssistantConstants.MediaType.Audio, mediaObject.MediaType);
            Assert.Equal(media.StreamUrl, mediaObject.MediaObjects.Single().ContentUrl);
            Assert.Equal(media.Subtitle, mediaObject.MediaObjects.Single().Description);
            Assert.Equal(media.Title, mediaObject.MediaObjects.Single().Name);
            Assert.Equal(media.LargeImageUrl, ((MediaObjectWithLargeImage)mediaObject.MediaObjects.Single()).Image.Url);
            Assert.Equal(media.Title, ((MediaObjectWithLargeImage)mediaObject.MediaObjects.Single()).Image.AccessibilityText);
        }

        private static Media CreateTestMedia()
        {
            var media = new Media
            {
                Author = "Popo The Clown",
                Subtitle = "The Sequel",
                StreamUrl = new SecureUri("https://www.awesome-sauce.com/most-podcast.mp3"),
                LargeImageUrl = new SecureUri("https://awesomeness/most-logo-large.png"),
                SmallImageUrl = new SecureUri("https://awesomeness/most-logo-small.png"),
                Title = "Clown Life",
                Token = Generic.Id()
            };
            return media;
        }

        private Media ExecuteProcessor(Action<IVirtualDirective, PlayMediaProcessor> action)
        {
            var media = CreateTestMedia();
            var virtualDirective = new PlayMediaDirective(media, PromptText.AsPrompt());
            var processor = new PlayMediaProcessor();
            action(virtualDirective, processor);
            return media;
        }

        private T GetItem<T>(AppResponse response) where T : RichResponseItem
        {
            return (T)response.Payload.Body.RichResponse.Items.SingleOrDefault(i => i is T);
        }
    }
}