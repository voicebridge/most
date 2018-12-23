using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK
{
    public class UserNotification
    {
        [JsonProperty("title")]
        public string Title { get; set; }
        
        [JsonProperty("text")]
        public string Text { get; set; }
    }
}