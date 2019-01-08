using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.Alexa
{
    public class UserEvent
    {
        [JsonProperty("source")]
        public UserEventSource Source { get; set; }
        
        [JsonProperty("arguments")]
        public string[] Arguments { get; set; }
    }
}