using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK
{
    public class GoogleDateTime
    {
        [JsonProperty("date")]
        public GoogleDate Date { get; set; }
        
        [JsonProperty("time")]
        public TimeOfDay Time { get; set; }
    }
}