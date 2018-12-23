using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK
{
    public class BasicCard
    {
        [JsonProperty("title")]
        public string Title { get; set; }
        
        [JsonProperty("subtitle")]
        public string Subtitle { get; set; }
        
        [JsonProperty("formattedText")]
        public string FormattedText { get; set; }
        
        [JsonProperty("image")]
        public Image Image { get; set; }
        
        [JsonProperty("buttons")]
        public Button[] Buttons { get; set; }
        
        [JsonProperty("imageDisplayOptions")]
        public string ImageDisplayOptions { get; set; }
        
    }
}