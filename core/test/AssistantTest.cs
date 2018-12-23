using System.Threading.Tasks;
using VoiceBridge.Most.Test.TestData;
using VoiceBridge.Most.VoiceModel.Alexa;
using Xunit;

namespace VoiceBridge.Most.Test
{
    public class AssistantTest
    {
        [Fact]
        public async Task HappyPath()
        {
            const string promptText = "56 degrees";
            var request = TestData.AlexaRequests.Weather("Menlo Park");
            
            var assistant = new Assistant();
            assistant
                .OnIntent(TestIntents.Weather)
                .When(x => x.RequestModel.ParameterHasValue("city"))
                .Say(promptText);
            var engine = assistant.AlexaEngineBuilder().Build();
            
            var response = await engine.Evaluate(request);
            Assert.True(response.Content.ShouldEndSession);
            Assert.Equal(promptText, ((PlainTextOutputSpeech)response.Content.OutputSpeech).Text);
        }
    }
}