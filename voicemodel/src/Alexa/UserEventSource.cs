using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.Alexa
{
    public class UserEventSource
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        
        [JsonProperty("handler")]
        public string Handler { get; set; }
        
        [JsonProperty("id")]
        public string Id { get; set; }
        
        [JsonProperty("value")]
        public string Value { get; set; }
    }
}