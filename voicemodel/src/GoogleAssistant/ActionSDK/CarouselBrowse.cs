using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK
{
    public class CarouselBrowse
    {
        [JsonProperty("items")]
        public CarouselItem[] CarouselItems { get; set; }
        
        [JsonProperty("imageDisplayOptions")]
        public string ImageDisplayOptions { get; set; }
    }
}