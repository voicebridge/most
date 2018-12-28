using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.Alexa.LanguageModel
{
    public class PromptValue
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        
        [JsonProperty("value")]
        public string Value { get; set; }
    }
}