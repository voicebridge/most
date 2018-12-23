using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK
{
    public class ExpectedIntent
    {
        [JsonProperty("intent")]
        public string Intent { get; set; }
        
        [JsonProperty("parameterName")]
        public string ParameterName { get; set; }
    }
}