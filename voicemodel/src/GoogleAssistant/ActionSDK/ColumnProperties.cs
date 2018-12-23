using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK
{
    public class ColumnProperties
    {
        [JsonProperty("header")]
        public string Header { get; set; }
        
        [JsonProperty("horizontalAlignment")]
        public string HorizontalAlignment { get; set; }
    }
}