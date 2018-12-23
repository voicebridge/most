using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK
{
    public class Capability
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}