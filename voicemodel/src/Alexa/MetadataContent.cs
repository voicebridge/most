using System.Collections.Generic;
using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.Alexa
{
    public class MetadataContent
    {
        [JsonProperty("sources")]
        public List<Source> Sources { get; set; }
    }
}