using VoiceBridge.Most.VoiceModel.Alexa;

namespace VoiceBridge.Most.Alexa
{
    public class AlexaAudioStatusChangeInputBuilder : IInputModelBuilder<SkillRequest>
    {
        public void Build(ConversationContext context, SkillRequest request)
        {
            if (request.Content?.Type == null ||
                !request.Content.Type.StartsWith(AlexaConstants.AudioPlayer.RequestTypePrefix))
            {
                return;
            }
            
            context.Extensions.Add(new AudioPlayerInfo
            {
                State = GetState(request),
                CurrentOffsetInMilliseconds = request.Context.AudioPlayer.OffsetInMilliseconds
            });
        }

        private AudioPlayerState GetState(SkillRequest request)
        {
            var state = request.Content.Type.Split('.')[1];
            switch (state)
            {
                case AlexaConstants.AudioPlayer.RequestTypes.Started:
                    return AudioPlayerState.Active;
                case AlexaConstants.AudioPlayer.RequestTypes.Finishing:
                    return AudioPlayerState.Finishing;
                case AlexaConstants.AudioPlayer.RequestTypes.Finished:
                    return AudioPlayerState.Finished;
                case AlexaConstants.AudioPlayer.RequestTypes.Stopped:
                    return AudioPlayerState.StoppedByUser;
                case AlexaConstants.AudioPlayer.RequestTypes.Failed:
                    return AudioPlayerState.Failed;
                default:
                    return AudioPlayerState.Other;
            }
        }
    }
}