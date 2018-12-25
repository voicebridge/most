using System.Linq;
using VoiceBridge.Most.VoiceModel.Alexa;

namespace VoiceBridge.Most.Alexa
{
    public class AlexaInputModelBuilder : IInputModelBuilder<SkillRequest>
    {
        public void Build(ConversationContext context, SkillRequest request)
        {
            context.RequestModel.RequestId = request.Content.RequestId;
            context.RequestModel.SessionId = request.Session.SessionId;
            context.RequestModel.UserId = request.Session.User.UserId;
            context.RequestModel.IntentName = 
                request.Content.Intent != null ? 
                    request.Content.Intent.Name : 
                    request.Content.Type;

            if (request.Content.Intent?.Slots == null) return;
            
            foreach (var key in request.Content.Intent.Slots.Keys)
            {
                var slot = request.Content.Intent.Slots[key];
                var value = new ParameterValue
                {
                    ProvidedValue = slot.Value, ResolvedId = slot.Value, ResolvedValue = slot.Value
                };
                context.RequestModel.Parameters[key] = value;
                var resolution = slot.Resolutions?.ResolutionsByAuthority?.FirstOrDefault();
                if (resolution == null) continue;
                if (resolution.Status.Code != AlexaConstants.SlotResolutionStatus.SuccessfulMatch) continue;
                var resolutionValue = resolution.Values.First();
                value.ResolvedId = resolutionValue.Id;
                value.ResolvedValue = resolutionValue.Name;
            }
        }
    }
}