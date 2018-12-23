using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.GoogleAssistant.DialogFlow
{
    public class ResponsePayload
    {
        [JsonProperty("google")] 
        public ResponseBody Body { get; set; }
    }
}