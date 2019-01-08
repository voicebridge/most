using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace VoiceBridge.Most.VoiceModel.Alexa.APL
{
    public class TemplateImage : ITemplateItem
    {
        public string Type => AlexaConstants.Presentation.TemplateItems.Types.Image;

        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("scale")]
        public string Scale { get; set; }

        [JsonProperty("width")]
        public string Width { get; set; }

        [JsonProperty("height")]
        public string Height { get; set; }

        [JsonProperty("align")]
        public string Align { get; set; }
    }
}
