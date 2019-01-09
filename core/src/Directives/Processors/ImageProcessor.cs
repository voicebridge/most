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
            // If the device doesn't support APL, bail
            if (!request.Context.System.Device.SupportedInterfaces.ContainsKey(AlexaConstants.DeviceInterfaceNames.AlexaPresentationLanguage))
                return;

            var output = new ShowImageDirective();
            var parent = new TemplateContainer(AlexaConstants.Presentation.Directions.Column);

            if (directive.Image is Image)
            {
                var image = directive.Image as Image;

                parent.Items.Add(BuildAplForImage(image.ImageUri.AbsoluteUri));
                output.Document.MainTemplate.Items.Add(parent);
                response.Content.Directives.Add(output);
                return;
            }

            if (directive.Image is ResponsiveImage)
            {
                var image = directive.Image as ResponsiveImage;

                if (image.SmallImageUri != null)
                    parent.Items.Add(BuildAplForImage(image.SmallImageUri.AbsoluteUri, AlexaConstants.Presentation.TemplateItems.ViewportClauses.SmallRound));

                if (image.MediumImageUri != null)
                    parent.Items.Add(BuildAplForImage(image.MediumImageUri.AbsoluteUri, AlexaConstants.Presentation.TemplateItems.ViewportClauses.MediumRectangle));

                if (image.LargeImageUri != null)
                    parent.Items.Add(BuildAplForImage(image.LargeImageUri.AbsoluteUri, AlexaConstants.Presentation.TemplateItems.ViewportClauses.LargeRectangle));

                if (image.ExtraLargeImageUri != null)
                    parent.Items.Add(BuildAplForImage(image.ExtraLargeImageUri.AbsoluteUri, AlexaConstants.Presentation.TemplateItems.ViewportClauses.ExtraLargeRectangle));

                if (parent.Items.Count == 0)
                    throw new ArgumentException("Responsive image require at least one Uri!");

                // Do we need to use one of the images as a "fallback"?
                if(parent.Items.Count < 4)
                {
                    // Yes - then use the last item in the list...
                    var last = parent.Items.Last() as TemplateContainer;

                    // ...by removing its sizing predicate (since we add them in size order above)
                    last.When = null;
                }

                output.Document.MainTemplate.Items.Add(parent);
                response.Content.Directives.Add(output);
                return;
            }

            throw new NotImplementedException();
        }


        /// <summary>
        /// Helper method to build an APL Image template for the given uri
        /// </summary>
        private static TemplateContainer BuildAplForImage(string uri, string predicate = "")
        {
            return new TemplateContainer()
            {
                When = (string.IsNullOrWhiteSpace(predicate) ? null : predicate),
                Grow = 1,
                AlignItems = AlexaConstants.Presentation.Alignment.Center,
                JustifyContent = AlexaConstants.Presentation.Justification.Center,
                Items = new List<ITemplateItem>()
                {
                    new TemplateImage()
                    {
                        Source = uri,
                        Scale = AlexaConstants.Presentation.Scale.BestFill,
                        Width = "100vw",
                        Height = "100vh",
                        Align = AlexaConstants.Presentation.Alignment.Center
                    }
                }
            };
        }

        protected override void Process(ImageDirective directive, AppRequest request, AppResponse response)
        {
            // At present, Google Home Hub doesn't support full-screen imagery
        }
    }
}