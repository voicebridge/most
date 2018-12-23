using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK
{
    public class TableCardItem
    {
        [JsonProperty("tableCard")]
        public TableCard Value { get; set; }
    }
}