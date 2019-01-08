using VoiceBridge.Most.VoiceModel.Alexa;

namespace VoiceBridge.Most.Alexa
{
    public class NonVoiceEventsInputModelBuilder : IInputModelBuilder<SkillRequest>
    {
        public void Build(ConversationContext context, SkillRequest request)
        {
            var input = GetSelectedDisplayElement(request) ?? GetAPLNonVoiceInput(request);

            if (input == null)
            {
                return;
            }

            context.Extensions.Add(input);
        }

        private NonVoiceInput GetAPLNonVoiceInput(SkillRequest request)
        {
            if (request.Content.Type != AlexaConstants.RequestType.AlexaPresentationLanguageUserEvent)
            {
                return null;
            }
            
            var input = new NonVoiceInput
            {
                SourceName = request.Content.EventInfo.Source.Id, 
                Values = request.Content.EventInfo.Arguments
            };

            return input;
        }

        private NonVoiceInput GetSelectedDisplayElement(SkillRequest request)
        {
            if (request.Content.Type != AlexaConstants.RequestType.AlexaDisplayElementSelected)
            {
                return null;
            }

            var input = new NonVoiceInput
            {
                SourceName = request.Content.EventToken,
                Values = new string[0]
            };
            
            return input;
        }
    }
}