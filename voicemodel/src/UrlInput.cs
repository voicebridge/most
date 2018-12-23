using Newtonsoft.Json;
using VoiceBridge.Most.VoiceModel.GoogleAssistant;
using VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK;

namespace VoiceBridge.Most.VoiceModel
{
    public class UrlInput : RawInput
    {
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}