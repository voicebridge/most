using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.Alexa
{
    public class SkillRequest : IRequest
    {
        [JsonProperty("version")]
        public string Version {get; set;}

        [JsonProperty("session")]
        public Session Session {get; set;}

        [JsonProperty("context")]
        public RequestContext Context {get; set;}

        [JsonProperty("request")]
        public RequestContent Content {get; set;}
    }
}