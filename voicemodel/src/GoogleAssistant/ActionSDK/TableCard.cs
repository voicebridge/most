using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK
{
    public class TableCard
    {
        [JsonProperty("title")]
        public string Title { get; set; }
        
        [JsonProperty("subtitle")]
        public string Subtitle { get; set; }
        
        [JsonProperty("image")]
        public Image Image { get; set; }
        
        [JsonProperty("columnProperties")]
        public ColumnProperties[] ColumnProperties { get; set; }
        
        [JsonProperty("rows")]
        public Row[] Rows { get; set; }
        
        [JsonProperty("buttons")]
        public Button[] Buttons { get; set; }
    }
}