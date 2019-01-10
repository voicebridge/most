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
    public class ShowImageTest : ImageTestBase
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

            Assert.True(image is Image);

            TestAplDocument(directive);

            var parent = directive.Document.MainTemplate.Items[0];
            TestParentContainer(parent, 1);

            var item = parent.Items[0] as TemplateContainer;
            TestContainerWithImage(item, IMAGE_URL_1);
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
            return new Image()
            {
                ImageUri = new SecureUri(IMAGE_URL_1)
            };
        }
    }
}
