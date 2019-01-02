using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK
{
    public class MediaObjectWithLargeImage : MediaObject
    {
        [JsonProperty("largeImage")]
        public Image Image { get; set; }
    }
}