using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK
{
    public class QueryInput : RawInput
    {
        [JsonProperty("query")]
        public string Query { get; set; }
    }
}