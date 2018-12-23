using Newtonsoft.Json;
using VoiceBridge.Most.VoiceModel.Alexa;
using VoiceBridge.Most.VoiceModel.GoogleAssistant;

namespace VoiceBridge.Most.VoiceModel
{
    public static class Serializer
    {
        private static readonly JsonSerializerSettings settings = new JsonSerializerSettings
        {
            MissingMemberHandling = MissingMemberHandling.Ignore
        };
        
        public static SkillRequest DeserializeRequest(string json)
        {
            return JsonConvert.DeserializeObject<SkillRequest>(json, settings);
        }
        
        public static string SerializeResponse(SkillResponse response)
        {
            return JsonConvert.SerializeObject(response, settings);
        }
        
        public static string SerializeResponseWithFormatting(SkillResponse response)
        {
            var responseSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };
            return JsonConvert.SerializeObject(response, responseSettings);
        }
    }
}