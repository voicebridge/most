using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace VoiceBridge.Most.VoiceModel.Alexa.APL
{
    public class TemplateContainer : ITemplateItem
    {
        public string Type => AlexaConstants.Presentation.TemplateItems.Types.Container;

        [JsonProperty("when", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string When { get; set; }

        [JsonProperty("direction", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Direction { get; set; }

        [JsonProperty("grow", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int Grow { get; set; }

        [JsonProperty("alignItems", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string AlignItems { get; set; }

        [JsonProperty("justifyContent", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string JustifyContent { get; set; }

        [JsonProperty("items")]
        public List<ITemplateItem> Items { get; set; } = new List<ITemplateItem>();
    }
}
