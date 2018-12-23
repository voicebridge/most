using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace VoiceBridge.Most.VoiceModel.Alexa
{
    public class AudioPlayer
    {
        [JsonProperty("token")]
        public string Token { get; set; }
        
        [JsonProperty("playerActivity")]
        public string Activity { get; set; }

        [JsonProperty("offsetInMilliseconds")]
        public int OffsetInMilliseconds { get; set; }
    }
}