using System.Collections.Generic;
using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.GoogleAssistant.DialogFlow
{
    public class AppResponse : IResponse
    {
        [JsonProperty("fulfillmentText", NullValueHandling = NullValueHandling.Ignore)]
        public string FulfillmentText { get; set; }
        
        [JsonProperty("fulfillmentMessages", NullValueHandling = NullValueHandling.Ignore)]
        public List<FulfillmentMessage> Messages { get; set; } 
        
        [JsonProperty("source", NullValueHandling = NullValueHandling.Ignore)]
        public string Source { get; set; }
        
        [JsonProperty("payload", NullValueHandling = NullValueHandling.Ignore)]
        public ResponsePayload Payload { get; set; }
    }
}