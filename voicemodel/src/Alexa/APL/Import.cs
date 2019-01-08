using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace VoiceBridge.Most.VoiceModel.Alexa.APL
{
    public class Import
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }
    }
}
