using System.Collections.Generic;
using System.Net.Mime;
using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.Alexa
{
    public class Session
    {
        [JsonProperty("new")]
        public bool New {get; set;}

        [JsonProperty("user")]
        public UserInfo User {get; set;}

        [JsonProperty("sessionId")]
        public string SessionId {get; set;}

        [JsonProperty("attributes")]
        public Dictionary<string, string> Attributes {get; set;}

        [JsonProperty("application")]
        public ApplicationInfo Application {get; set;}
    }
}