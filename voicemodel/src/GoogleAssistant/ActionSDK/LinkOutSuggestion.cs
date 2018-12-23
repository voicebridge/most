using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK
{
    public class LinkOutSuggestion
    {
        [JsonProperty("destinationName")]
        public string DestinationName { get; set; }
        
        [JsonProperty("openUrlAction")]
        public OpenUrlAction Action { get; set; }
    }
}