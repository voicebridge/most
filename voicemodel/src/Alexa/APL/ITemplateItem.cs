using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace VoiceBridge.Most.VoiceModel.Alexa.APL
{
    public interface ITemplateItem
    {
        [JsonProperty("type")]
        string Type { get; }
    }
}
