using System.Threading.Tasks;
using VoiceBridge.Most.Logging;
using VoiceBridge.Most.VoiceModel;
using VoiceBridge.Most.VoiceModel.Alexa;
using VoiceBridge.Most.VoiceModel.GoogleAssistant.DialogFlow;

namespace VoiceBridge.Most
{
    /// <summary>
    /// Composite Engine hosts multiple engines internally, it will invoke the appropriate
    /// engine depending on the request type
    /// </summary>
    public class CompositeEngine : ICompositeEngine
    {
        private readonly IConversationEngine<SkillRequest, SkillResponse> alexaEngine;
        private readonly IConversationEngine<AppRequest, AppResponse> googleEngine;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="alexaEngine">Instance of IConversationEngine setup for Alexa</param>
        /// <param name="googleEngine">Instance of IConversationEngine setup for Google</param>
        public CompositeEngine(
            IConversationEngine<SkillRequest, SkillResponse> alexaEngine,
            IConversationEngine<AppRequest, AppResponse> googleEngine
        )
        {
            this.alexaEngine = alexaEngine;
            this.googleEngine = googleEngine;
        }
        
        /// <summary>
        /// Evaluate and process request
        /// </summary>
        /// <param name="request">Request</param>
        /// <returns>Response</returns>
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