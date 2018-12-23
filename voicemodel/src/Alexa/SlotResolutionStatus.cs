using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.Alexa
{
    public class SlotResolutionStatus
    {
        [JsonProperty("code")]
        public string Code { get; set; }
    }
}