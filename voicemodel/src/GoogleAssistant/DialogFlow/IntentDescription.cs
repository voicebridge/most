using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.GoogleAssistant.DialogFlow
{
    public class IntentDescription
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("displayName")]
        public string DisplayName { get; set; }
    }
}