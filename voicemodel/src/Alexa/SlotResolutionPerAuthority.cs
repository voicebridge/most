using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace VoiceBridge.Most.VoiceModel.Alexa
{
    public class SlotResolutionPerAuthority
    {
        [JsonProperty("authority")]
        public string Authority { get; set; }

        [JsonProperty("status")]
        public SlotResolutionStatus Status { get; set; }
        
        [JsonProperty("values")]
        public List<SlotResolutionValue> Values { get; set; }
    }
}