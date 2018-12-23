using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK
{
    public abstract class Argument
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("rawText")]
        public string RawText { get; set; }
        
        [JsonProperty("textValue")]
        public string TextValue { get; set; }
        
        [JsonProperty("status")]
        public Status Status { get; set; }
    }
}