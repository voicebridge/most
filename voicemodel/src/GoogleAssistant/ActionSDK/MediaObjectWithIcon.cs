using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK
{
    public class MediaObjectWithIcon : MediaObject
    {
        [JsonProperty("icon")]
        public Image Image { get; set; }
    }
}