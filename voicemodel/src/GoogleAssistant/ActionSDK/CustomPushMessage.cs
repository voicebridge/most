using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK
{
    public class CustomPushMessage
    {
        [JsonProperty("target")]
        public Target Target { get; set; }
    }
}