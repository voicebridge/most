using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VoiceBridge.Common;
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
    public class ResponsiveImageTest : ImageTestBase
    {
        private const string IMAGE_URL_1 = "https://images-na.ssl-images-amazon.com/images/I/61GcN5yX6XL._SL1000_.jpg";
        private const string IMAGE_URL_2 = "https://images-na.ssl-images-amazon.com/images/I/6182S7MYC2L._SL1000_.jpg";
        private const string IMAGE_URL_3 = "https://images-na.ssl-images-amazon.com/images/I/61yI7vWa83L._SL1000_.jpg";
        private const string IMAGE_URL_4 = "https://images-na.ssl-images-amazon.com/images/I/51FQBzDZ3VL.jpg";


        [Fact]
        public void ShowImageOnAlexa()
        {
            var request = AlexaRequests.Boilerplate();
            request.Context.System.Device.SupportedInterfaces.Add(AlexaConstants.DeviceInterfaceNames.AlexaPresentationLanguage, "");

            var response = AlexaResponses.Boilerplate();
            var image = ExecuteProcessor((virtualDirective, processor) => processor.Process(virtualDirective, request, response));
            var directive = (ShowImageDirective)response.Content.Directives.Single(d => d is ShowImageDirective);

            Assert.True(image is ResponsiveImage);

            TestAplDocument(directive);

            var parent = directive.Document.MainTemplate.Items[0];
            TestParentContainer(parent, 4);

            var item = parent.Items[0] as TemplateContainer;
            TestContainerWithImage(item, IMAGE_URL_1, AlexaConstants.Presentation.TemplateItems.ViewportClauses.SmallRound);

            item = parent.Items[1] as TemplateContainer;
            TestContainerWithImage(item, IMAGE_URL_2, AlexaConstants.Presentation.TemplateItems.ViewportClauses.MediumRectangle);

            item = parent.Items[2] as TemplateContainer;
            TestContainerWithImage(item, IMAGE_URL_3, AlexaConstants.Presentation.TemplateItems.ViewportClauses.LargeRectangle);

            item = parent.Items[3] as TemplateContainer;
            TestContainerWithImage(item, IMAGE_URL_4, AlexaConstants.Presentation.TemplateItems.ViewportClauses.ExtraLargeRectangle);
        }


        [Fact]
        public void ShowImageOnAlexaWithFallback()
        {
            var request = AlexaRequests.Boilerplate();
            request.Context.System.Device.SupportedInterfaces.Add(AlexaConstants.DeviceInterfaceNames.AlexaPresentationLanguage, "");

            // LargeImageUri will "fall back" to ExtraLargeImageUri by the removal of the latter's "when" predicate
            var image = new ResponsiveImage()
            {
                SmallImageUri = new SecureUrl(IMAGE_URL_1),
                MediumImageUri = new SecureUrl(IMAGE_URL_2),
                ExtraLargeImageUri = new SecureUrl(IMAGE_URL_4),
            };

            var response = AlexaResponses.Boilerplate();
            var result = ExecuteProcessor((virtualDirective, processor) => processor.Process(virtualDirective, request, response), image);
            var directive = (ShowImageDirective)response.Content.Directives.Single(d => d is ShowImageDirective);

            Assert.Equal(image, result);

            TestAplDocument(directive);

            var parent = directive.Document.MainTemplate.Items[0];
            TestParentContainer(parent, 3);

            var item = parent.Items[0] as TemplateContainer;
            TestContainerWithImage(item, IMAGE_URL_1, AlexaConstants.Presentation.TemplateItems.ViewportClauses.SmallRound);

            item = parent.Items[1] as TemplateContainer;
            TestContainerWithImage(item, IMAGE_URL_2, AlexaConstants.Presentation.TemplateItems.ViewportClauses.MediumRectangle);

            item = parent.Items[2] as TemplateContainer;
            TestContainerWithImage(item, IMAGE_URL_4);
        }


        [Fact]
        public void ShowImageOnAlexaNullUrls()
        {
            var request = AlexaRequests.Boilerplate();
            request.Context.System.Device.SupportedInterfaces.Add(AlexaConstants.DeviceInterfaceNames.AlexaPresentationLanguage, "");

            var response = AlexaResponses.Boilerplate();

            Assert.Throws<ArgumentException>(() =>
            {
                var image = ExecuteProcessor((virtualDirective, processor) => processor.Process(virtualDirective, request, response), new ResponsiveImage());
            });
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
            var request = AppRequests.Boilerplate();
            var response = new ActionResponseFactory().Create(new ConversationContext());
            var image = ExecuteProcessor((virtualDirective, processor) => processor.Process(virtualDirective, request, response));

            Assert.Empty(response.Messages);
            Assert.Empty(response.Payload.Body.RichResponse.Items);
        }


        protected override IImage GetImage()
        {
            return new ResponsiveImage()
            {
                SmallImageUri = new SecureUrl(IMAGE_URL_1),
                MediumImageUri = new SecureUrl(IMAGE_URL_2),
                LargeImageUri = new SecureUrl(IMAGE_URL_3),
                ExtraLargeImageUri = new SecureUrl(IMAGE_URL_4),
            };
        }
    }
}
