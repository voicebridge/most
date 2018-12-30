using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.Alexa
{
    public class SlotResolution
    {
        [JsonProperty("value")]
        public SlotResolutionValue Value { get; set; }
    }
}