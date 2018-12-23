using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK
{
    public class CarouselItem
    {
        [JsonProperty("title")]
        public string Title { get; set; }
        
        [JsonProperty("description")]
        public string Description { get; set; }
        
        [JsonProperty("footer")]
        public string Footer { get; set; }
        
        [JsonProperty("image")]
        public Image Image { get; set; }
        
        [JsonProperty("openUrlAction")]
        public OpenUrlAction OpenUrlAction { get; set; }
    }
}