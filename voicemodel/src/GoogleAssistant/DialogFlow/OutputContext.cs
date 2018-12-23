using System.Collections.Generic;
using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.GoogleAssistant.DialogFlow
{
    public class OutputContext
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("lifespanCount")]
        public int LifespanCount { get; set; }
        
        [JsonProperty("parameters")]
        public Dictionary<string, string> Parameters { get; set; }
    }
}