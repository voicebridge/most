using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace VoiceBridge.Most.VoiceModel.Alexa.APL
{
    public class DocumentBody
    {
        [JsonProperty("type")]
        public string Type { get; set; } = "APL";

        [JsonProperty("version")]
        public string Version { get; set; } = "1.0";

        [JsonProperty("theme")]
        public string Theme { get; set; } = AlexaConstants.Presentation.Theme.Auto;

        [JsonProperty("import")]
        public List<Import> Import { get; set; } = new List<Import>();

        [JsonProperty("resources")]
        public List<Resource> Resources { get; set; } = new List<Resource>();

        [JsonProperty("styles")]
        public StyleCollection Styles { get; set; } = new StyleCollection();

        [JsonProperty("layouts")]
        public LayoutCollection Layouts { get; set; } = new LayoutCollection();

        [JsonProperty("mainTemplate", Order = 1)]
        public Template MainTemplate = new Template();
    }
}
