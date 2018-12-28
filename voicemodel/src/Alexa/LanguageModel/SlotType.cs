using System.Collections.Generic;
using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.Alexa.LanguageModel
{
    public class SlotType
    {
        public SlotType()
        {
            this.Values = new List<SlotChoice>();
        }
        
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("values")]
        public List<SlotChoice> Values { get; }
    }
}