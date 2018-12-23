using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK
{
    public class FinalResponse
    {
        [JsonProperty("richResponse")]
        public RichResponse RichResponse { get; set; }
    }
}