using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using VoiceBridge.Most.VoiceModel.Alexa.APL;

namespace VoiceBridge.Most.VoiceModel.Alexa.Directives
{
    public abstract class AplDirectiveBase : IAlexaDirective
    {
        [JsonProperty("type")]
        public abstract string Type { get; }

        [JsonProperty("document")]
        public virtual DocumentBody Document { get; private set; } = new DocumentBody();

        [JsonProperty("datasources", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public abstract List<DataSource> DataSources { get; }
    }
}
