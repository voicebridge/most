using System.Collections.Generic;
using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.Alexa.LanguageModel
{
    public class IntentDefinition
    {
        public IntentDefinition()
        {
            this.Samples = new List<string>();
            this.Slots = new List<SlotDefinition>();
        }
        
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("samples")]
        public List<string> Samples { get; }

        [JsonProperty("slots")]
        public List<SlotDefinition> Slots { get; }
    }
}