using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK
{
    public class Row
    {
        [JsonProperty("cells")]
        public Cell[] Cells { get; set; }
        
        [JsonProperty("dividerAfter")]
        public bool DividerAfter { get; set; }
    }
}