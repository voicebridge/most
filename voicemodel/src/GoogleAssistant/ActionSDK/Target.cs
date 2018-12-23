using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK
{
    public class Target
    {
        [JsonProperty("userId")]
        public string UserId { get; set; }
        
        [JsonProperty("intent")]
        public string Intent { get; set; }
        
        [JsonProperty("argument")]
        public Argument Argument { get; set; }
        
        [JsonProperty("locale")]
        public string Locale { get; set; }
    }
}