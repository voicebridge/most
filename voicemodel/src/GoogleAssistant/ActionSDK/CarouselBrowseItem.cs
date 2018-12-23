using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK
{
    public class CarouselBrowseItem
    {
        [JsonProperty("carouselBrowse")]
        public CarouselBrowse Value { get; set; }
    }
}