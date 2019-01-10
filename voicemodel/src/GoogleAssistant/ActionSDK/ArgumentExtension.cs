using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK
{
    public class ArgumentExtension
    {
        [JsonProperty("@type")]
        public string Type { get; set; }
        
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}