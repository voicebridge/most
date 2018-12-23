using System.Collections.Generic;
using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.GoogleAssistant.DialogFlow
{
    public class QueryResult
    {
        [JsonProperty("fulfillmentText")]
        public string FulfillmentText { get; set; }
        
        [JsonProperty("fulfillmentMessages")]
        public List<FulfillmentMessage> FulfillmentMessages { get; set; }
        
        [JsonProperty("queryText")]
        public string Text { get; set; }
        
        [JsonProperty("parameters")]
        public Dictionary<string, string> Parameters { get; set; }
        
        [JsonProperty("outputContext")]
        public List<OutputContext> OutputContexts { get; set; }
        
        [JsonProperty("intent")]
        public IntentDescription Intent { get; set; }
        
        [JsonProperty("intentDetectionConfidence")]
        public decimal IntentDetectionConfidence { get; set; }
        
        [JsonProperty("languageCode")]
        public string LanguageCode { get; set; }
    }
}