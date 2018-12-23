using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK
{
    public class Button
    {
        [JsonProperty("minVersion")]
        public string Title { get; set; }
        
        [JsonProperty("openUrlAction")]
        public OpenUrlAction OpenUrlAction { get; set; }
    }
}