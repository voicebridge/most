using System;
using System.Linq;
using VoiceBridge.Most.Alexa;
using VoiceBridge.Most.Google;
using VoiceBridge.Most.VoiceModel.Alexa;
using VoiceBridge.Most.VoiceModel.Alexa.Directives;
using VoiceBridge.Most.VoiceModel.GoogleAssistant;
using VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK;
using VoiceBridge.Most.VoiceModel.GoogleAssistant.DialogFlow;
using VoiceBridge.Most.VoiceModel.Alexa.APL;
using System.Collections.Generic;

namespace VoiceBridge.Most.Directives.Processors
{
    public class ImageProcessor : DirectiveProcessorBase<ImageDirective>
    {
        protected override void Process(ImageDirective directive, SkillRequest request, SkillResponse response)
        {
            response.Content.ShouldEndSession = !directive.KeepSessionOpen;

            if (directive.Image is Image)
            {
                var image = directive.Image as Image;
                var output = new ShowImageDirective();

                output.Document.MainTemplate.Items.Add(new TemplateContainer()
                {
                    Direction = AlexaConstants.Presentation.Directions.Column,
                    Items = new List<ITemplateItem>()
                    {
                        new TemplateContainer()
                        {
                            Grow = 1,
                            AlignItems = AlexaConstants.Presentation.Alignment.Center,
                            JustifyContent = AlexaConstants.Presentation.Justification.Center,
                            Items = new List<ITemplateItem>()
                            {
                                new TemplateImage()
                                {
                                    Source = image.ImageUri.AbsoluteUri,
                                    Scale = AlexaConstants.Presentation.Scale.BestFill,
                                    Width = "100vw",
                                    Height = "100vh",
                                    Align = AlexaConstants.Presentation.Alignment.Center
                                }
                            }
                        }
                    }
                });

                response.Content.Directives.Add(output);

                // TODO: Handle Caption
                return;
            }

            if (directive.Image is ResponsiveImage)
            {
                // TODO: Finish implementation
            }

            throw new NotImplementedException();
        }

        protected override void Process(ImageDirective directive, AppRequest request, AppResponse response)
        {
            // At present, Google Home Hub doesn't support full-screen imagery
        }
    }
}