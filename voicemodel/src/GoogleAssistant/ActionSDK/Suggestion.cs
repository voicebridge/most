using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK
{
    public class Suggestion
    {
        [JsonProperty("title")]
        public string Title { get; set; }
    }
}