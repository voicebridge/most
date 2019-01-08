using System.Linq;
using VoiceBridge.Most.VoiceModel.Alexa;

namespace VoiceBridge.Most.Alexa
{
    public class AlexaInputModelBuilder : IInputModelBuilder<SkillRequest>
    {
        public void Build(ConversationContext context, SkillRequest request)
        {
            ReadRequestType(context, request);
            ReadSession(context, request);
            ReadRequestMetadata(context, request);
            ReadSlots(context, request);
            ReadIntent(context, request);
        }

        private void ReadSession(ConversationContext context, SkillRequest request)
        { 
            if (request.Session == null)
            {
                return;
            }
         
            context.RequestModel.SessionId = request.Session.SessionId;
            
            if (request.Session.Attributes == null)
            {
                return;
            }
            
            foreach (var key in request.Session.Attributes.Keys)
            {
                context.SessionValues[key] = request.Session.Attributes[key];
            }
        }

        private void ReadIntent(ConversationContext context, SkillRequest request)
        {
            if (context.RequestType != RequestType.Intent)
            {
                return;
            }

            context.RequestModel.IntentName = request.Content.Intent.Name;
        }

        private void ReadRequestType(ConversationContext context, SkillRequest request)
        {
            switch (request.Content.Type)
            {
                case AlexaConstants.RequestType.SessionEndedRequest:
                    if (request.Content.Reason == AlexaConstants.SessionTerminationReasons.UserInitiated)
                    {
                        context.RequestType = RequestType.UserInitiatedTermination;
                    }

                    if (request.Content.Reason == AlexaConstants.SessionTerminationReasons.Error)
                    {
                        context.RequestType = RequestType.Error;
                    }

                    if (request.Content.Reason == AlexaConstants.SessionTerminationReasons.MaxPrepromptsExceeded)
                    {
                        context.RequestType = RequestType.Error;
                    }
                    
                    break;
                case AlexaConstants.RequestType.IntentRequest:
                    context.RequestType = RequestType.Intent;
                    break;
                
                case AlexaConstants.RequestType.LaunchRequest:
                    context.RequestType = RequestType.Launch;
                    break;
                
                case AlexaConstants.RequestType.CanFulfillIntentRequest:
                    context.RequestType = RequestType.FulfillmentQuery;
                    break;
                
                case AlexaConstants.RequestType.AlexaPresentationLanguageUserEvent:
                    context.RequestType = RequestType.NonVoiceInputEvent;
                    break;
                
                default:
                    context.RequestType = RequestType.Other;
                    break;
            }
        }

        private static void ReadSlots(ConversationContext context, SkillRequest request)
        {
            if (request.Content.Intent?.Slots == null)
            {
                return;
            }

            foreach (var key in request.Content.Intent.Slots.Keys)
            {
                var slot = request.Content.Intent.Slots[key];
                var value = new ParameterValue
                {
                    ProvidedValue = slot.Value, 
                    ResolvedId = slot.Value, 
                    ResolvedValue = slot.Value
                };
                context.RequestModel.Parameters[key] = value;
                var resolution = slot.Resolutions?.ResolutionsByAuthority?.FirstOrDefault();
    
                if (resolution?.Status?.Code != AlexaConstants.SlotResolutionStatus.SuccessfulMatch)
                {
                    continue;
                }
                
                var resolutionValue = resolution.Values.First().Value;
                value.ResolvedId = resolutionValue.Id ?? value.ResolvedId;
                value.ResolvedValue = resolutionValue.Name ?? value.ResolvedValue;
            }
        }

        private static void ReadRequestMetadata(ConversationContext context, SkillRequest request)
        {
            context.RequestModel.RequestId = request.Content.RequestId;
            context.RequestModel.UserId = request.Context.System.User.UserId;
        }
    }
}