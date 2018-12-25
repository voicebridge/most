using System.Collections.Generic;
using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK
{
    public class Surface
    {
        [JsonProperty("capabilities")]
        public List<Capability> Capabilities { get; set; }
    }
}