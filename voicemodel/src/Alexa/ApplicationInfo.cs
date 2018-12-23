using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.Alexa
{
    public class ApplicationInfo
    {
        [JsonProperty("applicationID")]
        public string ApplicationId {get; set;}
    }
}