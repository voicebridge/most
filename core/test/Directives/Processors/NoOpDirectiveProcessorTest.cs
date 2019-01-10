using VoiceBridge.Most.Directives;
using VoiceBridge.Most.Directives.Processors;
using VoiceBridge.Most.Test.TestData;
using VoiceBridge.Most.VoiceModel.GoogleAssistant.DialogFlow;
using Xunit;

namespace VoiceBridge.Most.Test.Directives.Processors
{
    public class NoOpDirectiveProcessorTest
    {
        [Fact]
        public void AlexaRequest()
        {
            var request = AlexaRequests.Boilerplate();
            var response = AlexaResponses.Boilerplate();
            var processor = new NoOpDirectiveProcessor();
            processor.Process(new NoOpDirective(), request, response);
            Assert.Null(response.Content);
            Assert.Null(response.SessionAttributes);
        }

        [Fact]
        public void AssistantRequest()
        {
            var request = AppRequests.Boilerplate();
            var response = new AppResponse();
            var processor = new NoOpDirectiveProcessor();
            processor.Process(new NoOpDirective(), request, response);
            Assert.Null(response.Source);
            Assert.Null(response.Payload);
            Assert.Null(response.Messages);
            Assert.Null(response.FulfillmentText);
        }
    }
}