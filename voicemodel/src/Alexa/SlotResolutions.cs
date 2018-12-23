using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace VoiceBridge.Most.VoiceModel.Alexa
{
    public class SlotResolutions
    {
        [JsonProperty("resolutionsPerAuthority")]
        public List<SlotResolutionPerAuthority> ResolutionsByAuthority { get; set; }
    }
}