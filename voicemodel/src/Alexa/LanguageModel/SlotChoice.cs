using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.Alexa.LanguageModel
{
    public class SlotChoice
    {
        public SlotChoice()
        {
            this.Value = new SlotValue();
        }
        
        [JsonProperty("id")]
        public string Id { get; set; }
        
        [JsonProperty("name")]
        public SlotValue Value { get; }
    }
}