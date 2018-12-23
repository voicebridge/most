using System.Collections.Generic;
using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.Alexa
{
    public class SkillResponse : IResponse
    {
        [JsonProperty("version")]
        public string Version {get; set;}
        
        [JsonProperty("sessionAttributes", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string> SessionAttributes {get; set;}

        [JsonProperty("response")]
        public ResponseContent Content {get; set;}
    }
}