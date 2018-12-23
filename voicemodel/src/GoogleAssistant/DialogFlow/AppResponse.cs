using System.Collections.Generic;
using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.GoogleAssistant.DialogFlow
{
    public class AppResponse : IResponse
    {
        [JsonProperty("fulfillmentText")]
        public string FulfillmentText { get; set; }
        
        [JsonProperty("fulfillmentMessages")]
        public List<FulfillmentMessage> Messages { get; set; } 
        
        [JsonProperty("source")]
        public string Source { get; set; }
        
        [JsonProperty("payload")]
        public ResponsePayload Payload { get; set; }
    }
}