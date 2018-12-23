using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.Alexa
{
    public class Slot
    {
        [JsonProperty("name")] 
        public string Name { get; set; }

        [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)] 
        public string Value { get; set; }

        [JsonProperty("confirmationStatus")]
        public string ConfirmationStatus { get; set; }

        [JsonProperty("resolutions", NullValueHandling = NullValueHandling.Ignore)]
        public SlotResolutions Resolutions { get; set; }
    }
}