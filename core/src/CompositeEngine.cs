using System.Threading.Tasks;
using VoiceBridge.Most.Logging;
using VoiceBridge.Most.VoiceModel;
using VoiceBridge.Most.VoiceModel.Alexa;
using VoiceBridge.Most.VoiceModel.GoogleAssistant.DialogFlow;

namespace VoiceBridge.Most
{
    public class CompositeEngine : ICompositeEngine
    {
        private readonly IConversationEngine<SkillRequest, SkillResponse> alexaEngine;
        private readonly IConversationEngine<AppRequest, AppResponse> googleEngine;

        public CompositeEngine(
            IConversationEngine<SkillRequest, SkillResponse> alexaEngine,
            IConversationEngine<AppRequest, AppResponse> googleEngine
        )
        {
            this.alexaEngine = alexaEngine;
            this.googleEngine = googleEngine;
        }
        
        public async Task<IResponse> Evaluate(IRequest request)
        {
            if (request.IsAlexaRequest())
            {
                return await this.alexaEngine.Evaluate((SkillRequest) request);
            }

            return await this.googleEngine.Evaluate((AppRequest) request);
        }
    }
}