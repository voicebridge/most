using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.GoogleAssistant.DialogFlow
{
    public class AppRequest : IRequest
    {
        [JsonProperty("responseId")]
        public string ResponseId { get; set; }
        
        [JsonProperty("session")]
        public string SessionId { get; set; }
        
        [JsonProperty("queryResult")]
        public QueryResult Result { get; set; }
        
        [JsonProperty("originalDetectIntentRequest")]
        public Payload OriginalDetectIntentRequest { get; set; }
    }
}