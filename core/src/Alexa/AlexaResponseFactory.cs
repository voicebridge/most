using System.Collections.Generic;
using VoiceBridge.Most.VoiceModel.Alexa;

namespace VoiceBridge.Most.Alexa
{
    internal class AlexaResponseFactory : IResponseFactory<SkillResponse>
    {
        public SkillResponse Create(ConversationContext context)
        {
            var response = new SkillResponse
            {
                Version = AlexaConstants.AlexaVersion,
                Content = new ResponseContent
                {
                    Directives = new List<IAlexaDirective>()
                },
                SessionAttributes = new Dictionary<string, string>()
            };
            TransferSessionValues(context, response);
            return response;
        }

        private void TransferSessionValues(ConversationContext context, SkillResponse response)
        {
            foreach (var key in context.SessionValues.Keys)
            {
                response.SessionAttributes[key] = context.SessionValues[key];
            }
        }
    }
}