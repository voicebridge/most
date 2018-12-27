using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.Alexa
{
    public class AlexaError
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}