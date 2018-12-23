using System.Linq;
using System.Net.Mime;
using VoiceBridge.Most.VoiceModel.GoogleAssistant;
using VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK;
using VoiceBridge.Most.VoiceModel.GoogleAssistant.DialogFlow;

namespace VoiceBridge.Most.Google
{
    public class ActionInputModelBuilder : IInputModelBuilder<AppRequest>
    {
        public void Build(ConversationContext context, AppRequest request)
        {
            context.RequestModel.UserId = request.OriginalDetectIntentRequest.Content.User.UserId;
            context.RequestModel.Locale = request.Result.LanguageCode;
            context.RequestModel.InteractionId = request.ResponseId;
            context.RequestModel.IntentName = request.Result.Intent.DisplayName;
            if (request.Result.Parameters == null)
            {
                return;
            }
            
            foreach (var arg in request.Result.Parameters.Keys)
            {
                var value = request.Result.Parameters[arg];
                var param = new ParameterValue
                {
                    ProvidedValue = value, 
                    ResolvedId = value, 
                    ResolvedValue = value
                };

                context.RequestModel.Parameters[arg] = param;
            }
        }
    }
}