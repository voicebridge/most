using System;
using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.Alexa
{
    public class Source
    {
        [JsonProperty("url")]
        public Uri Url { get; set; }
    }
}