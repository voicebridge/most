using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.Alexa
{
    public class AudioItem
    {
        [JsonProperty("stream")]
        public AlexaStream StreamInfo { get; set; }
        
        [JsonProperty("metadata")]
        public AlexaStreamMetadata Metadata { get; set; }
    }
}