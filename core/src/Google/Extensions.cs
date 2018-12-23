using VoiceBridge.Most.VoiceModel.GoogleAssistant;
using VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK;

namespace VoiceBridge.Most.Google
{
    public static class Extensions
    {
        public static SimpleResponse ToSimpleResponse(this string s)
        {
            return new SimpleResponse
            {
                DisplayText = s,
                TextToSpeech = s
            };
        }

        public static SimpleResponse ToSimpleResponse(this Prompt p)
        {
            if (p.IsSSML)
            {
                return new SimpleResponse
                {
                    SSML = p.Content
                };
            }

            return new SimpleResponse
            {
                DisplayText = p.Content,
                TextToSpeech = p.Content
            };
        }
    }
}