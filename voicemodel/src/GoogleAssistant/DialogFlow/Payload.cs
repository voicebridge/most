using Newtonsoft.Json;
using VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK;

namespace VoiceBridge.Most.VoiceModel.GoogleAssistant.DialogFlow
{
    public class Payload
    {
        [JsonProperty("source")]
        public string Source { get; set; }
        
        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("payload")]
        public ActionRequest Content { get; set; }
    }
}