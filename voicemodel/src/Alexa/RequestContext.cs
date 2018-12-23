using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.Alexa
{
    public class RequestContext
    {
        [JsonProperty("System")]
        public AlexaInfo System {get; set;}

        [JsonProperty("AudioPlayer")]
        public AudioPlayer AudioPlayer { get; set; }
    }
}