using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.GoogleAssistant
{
    public class Message
    {
        [JsonProperty("text")]
        public string Content { get; set; }
    }
}