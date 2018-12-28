using System.Collections.Generic;
using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.Alexa.LanguageModel
{
    public class SlotDefinition
    {
        public SlotDefinition()
        {
            this.Samples = new List<string>();
        }
        
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("type")]
        public string Type { get; set; }
        
        [JsonProperty("samples")]
        public List<string> Samples { get; }
    }
}