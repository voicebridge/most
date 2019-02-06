using System;
using System.Linq;
using VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK;
using VoiceBridge.Most.VoiceModel.GoogleAssistant.DialogFlow;

namespace VoiceBridge.Most.Google
{
    internal class NonVoiceInputModelBuilder : IInputModelBuilder<AppRequest>
    {
        public void Build(ConversationContext context, AppRequest request)
        {
            if (request.Result?.Text != GoogleAssistantConstants.EventNames.DisplayElementSelection)
            {
                return;
            }

            var input = request.OriginalDetectIntentRequest.Content.Inputs.Single(
                x => x.Intent == "actions.intent.OPTION");
            var sourceName = input.Arguments.Single(x => x.Name == "OPTION").TextValue;
            
            context.Extensions.Add(new NonVoiceInput
            {
                SourceName = sourceName,
                Values = new string[0]
            });
        }
    }
}