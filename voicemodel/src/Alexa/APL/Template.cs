using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace VoiceBridge.Most.VoiceModel.Alexa.APL
{
    public class Template
    {
        [JsonProperty("parameters")]
        public List<string> Parameters = new List<string>();

        [JsonProperty("items")]
        public List<TemplateContainer> Items = new List<TemplateContainer>();
    }
}
