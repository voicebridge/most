using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK
{
    public class Surface
    {
        [JsonProperty("capabilities")]
        public Capability[] Capabilities { get; set; }
    }
}