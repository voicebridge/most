using System.Linq;
using VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK;
using VoiceBridge.Most.VoiceModel.GoogleAssistant.DialogFlow;

namespace VoiceBridge.Most.Google
{
    public class GoogleAudioStatusChangeInputBuilder : IInputModelBuilder<AppRequest>
    {
        public void Build(ConversationContext context, AppRequest request)
        {
            var input = request.OriginalDetectIntentRequest?.Content?.Inputs?.FirstOrDefault(x =>
                x.Intent == GoogleAssistantConstants.CommonIntents.MediaStatusChange);
            
            if (input?.Arguments == null)
            {
                return;
            }

            var targetArgument = input.Arguments.FirstOrDefault(x => x.Name == "MEDIA_STATUS");
            if (targetArgument == null)
            {
                return;
            }

            var playerInfo = new AudioPlayerInfo();
            context.Extensions.Add(playerInfo);
            switch (targetArgument.Extension.Status)
            {
                case "FINISHED":
                    playerInfo.State = AudioPlayerState.Finished;
                    break;
                
                case "FAILED":
                    playerInfo.State = AudioPlayerState.Failed;
                    break;
                
                default:
                    playerInfo.State = AudioPlayerState.Other;
                    break;
            }
        }
    }
}