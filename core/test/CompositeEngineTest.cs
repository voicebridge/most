using System.Threading.Tasks;
using Moq;
using VoiceBridge.Most.VoiceModel.Alexa;
using VoiceBridge.Most.VoiceModel.GoogleAssistant.DialogFlow;
using Xunit;
using Xunit.Abstractions;

namespace VoiceBridge.Most.Test
{
    public class CompositeEngineTest : TestBase
    {
        private readonly SkillRequest testAlexaRequest = new SkillRequest();
        private readonly SkillResponse testAlexaResponse = new SkillResponse();
        private readonly AppRequest testGoogleRequest = new AppRequest();
        private readonly AppResponse testGoogleResponse = new AppResponse();

        [Fact]
        public async Task CompositeEngineHandlesAlexaRequest()
        {
            var engine = GetEngine();
            var response = await engine.Evaluate(testAlexaRequest);
            Assert.Same(testAlexaResponse, response);
        }

        [Fact]
        public async Task CompositeEngineHandlesGoogleRequest()
        {
            var engine = GetEngine();
            var response = await engine.Evaluate(testGoogleRequest);
            Assert.Same(testGoogleResponse, response);
        }
        
        private ICompositeEngine GetEngine()
        {
            var alexa = new Mock<IConversationEngine<SkillRequest, SkillResponse>>();
            alexa.Setup(x => x.Evaluate(testAlexaRequest)).Returns(Task.Factory.StartNew(() => testAlexaResponse));
            var google = new Mock<IConversationEngine<AppRequest, AppResponse>>();
            google.Setup(x => x.Evaluate(testGoogleRequest)).Returns(Task.Factory.StartNew(() => testGoogleResponse));
            return new CompositeEngine(alexa.Object, google.Object);
        }

        public CompositeEngineTest(ITestOutputHelper output) : base(output)
        {
        }
    }

}