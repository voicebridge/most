using System.Collections.Generic;
using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.Alexa.LanguageModel
{
    public class PromptDefinition
    {
        public PromptDefinition()
        {
            this.Variations = new List<PromptValue>();
        }
        
        [JsonProperty("id")]
        public string Id { get; set; }
        
        [JsonProperty("variations")]
        public List<PromptValue> Variations { get; }
    }
}