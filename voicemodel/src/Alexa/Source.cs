using System;
using Newtonsoft.Json;
using VoiceBridge.Common;

namespace VoiceBridge.Most.VoiceModel.Alexa
{
    public class Source
    {
        [JsonProperty("url")]
        public SecureUri Url { get; set; }
    }
}