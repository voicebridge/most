using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK
{
    public class Icon : MediaObject
    {
        [JsonProperty("icon")]
        public Image Image { get; set; }
    }
}