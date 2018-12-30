using System;
using System.Linq;
using VoiceBridge.Most.Directives;
using VoiceBridge.Most.Directives.Processors;
using VoiceBridge.Most.Test.TestData;
using VoiceBridge.Most.VoiceModel.Alexa;
using VoiceBridge.Most.VoiceModel.Alexa.Directives;
using Xunit;

namespace VoiceBridge.Most.Test.Directives.Processors
{
    public class PlayMediaProcessorTest
    {
        [Fact]
        public void PlayMediaOnAlexa()
        {
            var request = AlexaRequests.Boilerplate();
            var response = AlexaResponses.Boilerplate();
            var media = new Media
            {
                Author = "Popo The Clown",
                Subtitle = "The Sequel",
                StreamUrl = new Uri("https://www.awesome-sauce.com/most-podcast.mp3"),
                LargeImageUrl = new Uri("https://awesomeness/most-logo-large.png"),
                SmallImageUrl = new Uri("https://awesomeness/most-logo-small.png"),
                Title = "Clown Life",
                Token = Generic.Id()
            };

            var virtualDirective = new PlayMediaDirective(media);
            var processor = new PlayMediaProcessor();
            processor.Process(virtualDirective, request, response);
            var directive = (PlayAudioDirective) response.Content.Directives.Single(d => d is PlayAudioDirective);
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
        }
    }
}