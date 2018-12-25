using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using Newtonsoft.Json;
using VoiceBridge.Most.VoiceModel.GoogleAssistant;
using VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK;
using VoiceBridge.Most.VoiceModel.GoogleAssistant.DialogFlow;

namespace VoiceBridge.Most.Google
{
    public class ActionInputModelBuilder : IInputModelBuilder<AppRequest>
    {
        public void Build(ConversationContext context, AppRequest request)
        {
            ReadMetadata(context, request);
            ReadParameters(context, request);
            ReadSessionData(context, request);
        }

        private void ReadSessionData(ConversationContext context, AppRequest request)
        {
            if (request.OriginalDetectIntentRequest?.Content?.User == null)
            {
                return;
            }

            var json = request.OriginalDetectIntentRequest.Content.User.UserStorage;
            if (string.IsNullOrWhiteSpace(json))
            {
                return;
            }

            var data = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            foreach (var key in data.Keys)
            {
                context.SessionStore[key] = data[key];
            }
        }

        private void ReadParameters(ConversationContext context, AppRequest request)
        {
            if (request.Result?.Parameters == null)
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

        private static void ReadMetadata(ConversationContext context, AppRequest request)
        {
            context.RequestModel.SessionId = request.SessionId;
            context.RequestModel.UserId = request.OriginalDetectIntentRequest.Content.User.UserId;
            context.RequestModel.Locale = request.Result.LanguageCode;
            context.RequestModel.RequestId = request.ResponseId;
            context.RequestModel.IntentName = request.Result.Intent.DisplayName;
        }
    }
}