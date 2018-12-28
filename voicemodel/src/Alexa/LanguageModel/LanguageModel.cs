using System.Collections.Generic;
using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.Alexa.LanguageModel
{
    public class LanguageModel
    {
        public LanguageModel()
        {
            this.Intents = new List<IntentDefinition>();
            this.Types = new List<SlotType>();
        }

        [JsonProperty("invocationName")]
        public string InvocationName { get; set; }
        
        [JsonProperty("intents")]
        public List<IntentDefinition> Intents { get; }
        
        [JsonProperty("types")]
        public List<SlotType> Types { get; }
    }
}