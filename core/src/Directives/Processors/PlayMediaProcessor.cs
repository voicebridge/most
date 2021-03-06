using System.Collections.Generic;
using System.Linq.Expressions;
using VoiceBridge.Most.Alexa;
using VoiceBridge.Most.VoiceModel.Alexa;
using VoiceBridge.Most.VoiceModel.Alexa.Directives;
using VoiceBridge.Most.VoiceModel.Alexa.Directives.AudioPlayer;
using VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK;
using VoiceBridge.Most.VoiceModel.GoogleAssistant.DialogFlow;

namespace VoiceBridge.Most.Directives.Processors
{
    internal class PlayMediaProcessor : DirectiveProcessorBase<PlayMediaDirective>
    {
        protected override void Process(PlayMediaDirective directive, SkillRequest request, SkillResponse response)
        {
            var alexaDirective = CreateAlexaPlayAudioDirective();   
            SetupStream(alexaDirective, directive);
            SetupStreamMetadata(alexaDirective, directive);
            response.Content.OutputSpeech = directive.Prompt.ToAlexaSpeech();
            response.Content.Directives.Add(alexaDirective);
        }

        protected override void Process(PlayMediaDirective directive, AppRequest request, AppResponse response)
        {
            var mediaResponseItem = new MediaResponseItem
            {
                Value = new MediaResponse
                {
                    MediaType = GoogleAssistantConstants.MediaType.Audio,
                    MediaObjects = new List<MediaObject>
                    {
                        new MediaObjectWithLargeImage
                        {
                            ContentUrl = directive.Media.StreamUrl,
                            Description = directive.Media.Subtitle,
                            Name = directive.Media.Title,
                            Image = new VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK.Image
                            {
                                AccessibilityText = directive.Media.Title,
                                Url = directive.Media.LargeImageUrl
                            }
                        }
                    }
                }
            };

            response.Payload.Body.RichResponse.Items.Add(directive.Prompt.ToAssistantSimpleResponse());
            response.Payload.Body.RichResponse.Items.Add(mediaResponseItem);
        }

        private static PlayAudioDirective CreateAlexaPlayAudioDirective()
        {
            var alexaDirective = new PlayAudioDirective
            {
                PlayBehavior = AlexaConstants.AudioPlayer.AudioPlayerBehavior.ReplaceAll,
                Audio = new AudioItem
                {
                    StreamInfo = new AlexaStream(),
                    Metadata = new AlexaStreamMetadata
                    {
                        Art = new MetadataContent{ Sources = new List<Source>() }
                    }
                }
            };
            return alexaDirective;
        }

        private void SetupStream(PlayAudioDirective alexaDirective, PlayMediaDirective directive)
        {
            alexaDirective.Audio.StreamInfo.Token = directive.Media.Token;
            alexaDirective.Audio.StreamInfo.Stream = directive.Media.StreamUrl;
            alexaDirective.Audio.StreamInfo.ExpectedPreviousToken = null;
            alexaDirective.Audio.StreamInfo.OffsetInMilliseconds = 0;
        }

        private void SetupStreamMetadata(PlayAudioDirective alexaDirective, PlayMediaDirective directive)
        {
            alexaDirective.Audio.Metadata.Title = directive.Media.Title;
            alexaDirective.Audio.Metadata.Subtitle = directive.Media.Subtitle;
            alexaDirective.Audio.Metadata.Art.Sources.Add(new Source {Url = directive.Media.LargeImageUrl});
        }
    }
}