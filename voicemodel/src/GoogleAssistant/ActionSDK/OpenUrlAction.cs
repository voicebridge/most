using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK
{
    public class OpenUrlAction
    {
        [JsonProperty("url")]
        public string Url { get; set; }
        
        [JsonProperty("androidApp")]
        public AndroidApp AndroidApp { get; set; }
        
        [JsonProperty("urlTypeHint")]
        public string UrlTypeHint { get; set; }
    }
}