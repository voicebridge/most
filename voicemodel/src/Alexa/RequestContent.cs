using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace VoiceBridge.Most.VoiceModel.Alexa
{
    public class RequestContent
    {
        [JsonProperty("type")]
        public string Type {get; set;}

        [JsonProperty("requestId")]
        public string RequestId {get; set;}

        [JsonProperty("timestamp")]
        public DateTime Timestamp {get; set;}

        [JsonProperty("locale")]
        public string Locale {get; set;}

        [JsonProperty("intent")]
        public Intent Intent {get; set;}

        [JsonProperty("updatedintent")]
        public Intent UpdatedIntent { get; set; }

        [JsonProperty("reason")]
        public string Reason { get; set; }

        [JsonProperty("dialogState")]
        public string DialogState { get; set; }

        [JsonProperty("error")]
        public AlexaError Error { get; set; }
        
        [JsonProperty("token")]
        public string EventToken { get; set; }
        
        [JsonProperty("event")]
        public UserEvent EventInfo { get; set; }
    }
}