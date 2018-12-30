using VoiceBridge.Most.Alexa;
using VoiceBridge.Most.VoiceModel.Alexa;

namespace VoiceBridge.Most.Test.TestData
{
    public static class AlexaResponses
    {
        public static SkillResponse Boilerplate()
        {
            var factory = new AlexaResponseFactory();
            return factory.Create(new ConversationContext());
        }
    }
}