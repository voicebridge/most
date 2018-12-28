using System.Collections.Generic;
using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.Alexa.LanguageModel
{
    public class SlotValue
    {
        public SlotValue()
        {
            this.Synonyms = new List<string>();
        }
        
        [JsonProperty("value")]
        public string Value { get; set; }
        
        [JsonProperty("synonyms")]
        public List<string> Synonyms { get; }
    }
}