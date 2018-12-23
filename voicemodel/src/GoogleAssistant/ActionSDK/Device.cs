using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK
{
    public class Device
    {
        [JsonProperty("location")]
        public Location Location { get; set; }
    }
}