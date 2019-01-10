using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VoiceBridge.Most.Directives;
using VoiceBridge.Most.Directives.Processors;
using VoiceBridge.Most.Google;
using VoiceBridge.Most.Test.TestData;
using VoiceBridge.Most.VoiceModel.Alexa;
using VoiceBridge.Most.VoiceModel.Alexa.APL;
using VoiceBridge.Most.VoiceModel.Alexa.Directives;
using Xunit;

namespace VoiceBridge.Most.Test.Directives.Processors
{
    public class ShowImageTest
    {
        private const string IMAGE_URL_1 = "https://images-na.ssl-images-amazon.com/images/I/61GcN5yX6XL._SL1000_.jpg";


        [Fact]
        public void ShowImageOnAlexa()
        {
            var request = AlexaRequests.Boilerplate();
            request.Context.System.Device.SupportedInterfaces.Add(AlexaConstants.DeviceInterfaceNames.AlexaPresentationLanguage, "");

            var response = AlexaResponses.Boilerplate();
            var image = ExecuteProcessor((virtualDirective, processor) => processor.Process(virtualDirective, request, response));
            var directive = (ShowImageDirective)response.Content.Directives.Single(d => d is ShowImageDirective);

            Assert.Null(directive.DataSources);
            Assert.Equal(AlexaConstants.Presentation.Directives.RenderDocument, directive.Type);
            Assert.Equal("1.0", directive.Document.Version);
            Assert.Equal("APL", directive.Document.Type);
            Assert.Equal("auto", directive.Document.Theme);
            Assert.Empty(directive.Document.Resources);
            Assert.Single(directive.Document.Import);
            Assert.Equal("1.0.0", directive.Document.Import[0].Version);
            Assert.Equal("alexa-viewport-profiles", directive.Document.Import[0].Name);
            Assert.Empty(directive.Document.MainTemplate.Parameters);
            Assert.Single(directive.Document.MainTemplate.Items);

            var item = directive.Document.MainTemplate.Items[0];
            Assert.Single(item.Items);
            Assert.True(string.IsNullOrWhiteSpace(item.AlignItems));
            Assert.Equal("column", item.Direction);
            Assert.Equal(0, item.Grow);
            Assert.True(string.IsNullOrWhiteSpace(item.JustifyContent));
            Assert.Equal("Container", item.Type);
            Assert.Null(item.When);

            item = item.Items[0] as TemplateContainer;
            Assert.Single(item.Items);
            Assert.Equal("center", item.AlignItems);
            Assert.True(string.IsNullOrWhiteSpace(item.Direction));
            Assert.Equal(1, item.Grow);
            Assert.Equal("center", item.JustifyContent);
            Assert.Equal("Container", item.Type);
            Assert.Null(item.When);

            var tImage = item.Items[0] as TemplateImage;
            Assert.Equal("center", tImage.Align);
            Assert.Equal("100vh", tImage.Height);
            Assert.Equal("best-fill", tImage.Scale);
            Assert.Equal(IMAGE_URL_1, tImage.Source);
            Assert.Equal("Image", tImage.Type);
            Assert.Equal("100vw", tImage.Width);
        }

        [Fact]
        public void ShowImageOnAlexaNoSupport()
        {
            var request = AlexaRequests.Boilerplate();
            var response = AlexaResponses.Boilerplate();
            var image = ExecuteProcessor((virtualDirective, processor) => processor.Process(virtualDirective, request, response));

            Assert.Empty(response.Content.Directives);
        }


        [Fact]
        public void ShowImageOnGoogle()
        {
            var request = AppRequests.CreateBoileRequest();
            var response = new ActionResponseFactory().Create(new ConversationContext());
            var image = ExecuteProcessor((virtualDirective, processor) => processor.Process(virtualDirective, request, response));

            Assert.Empty(response.Messages);
            Assert.Empty(response.Payload.Body.RichResponse.Items);
        }

        private Image GetImage()
        {
            return new Image()
            {
                ImageUri = new Uri(IMAGE_URL_1)
            };
        }

        private Image ExecuteProcessor(Action<IVirtualDirective, ImageProcessor> action)
        {
            var image = GetImage();
            var virtualDirective = new ImageDirective()
            {
                Image = image
            };
            
            var processor = new ImageProcessor();
            action(virtualDirective, processor);
            return image;
        }
    }
}
