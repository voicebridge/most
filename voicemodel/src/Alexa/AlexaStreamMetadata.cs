using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.Alexa
{
    public class AlexaStreamMetadata
    {
        [JsonProperty("title")]
        public string Title { get; set; }
        
        [JsonProperty("subtitle")]
        public string Subtitle { get; set; }
        
        [JsonProperty("art")]
        public MetadataContent Art { get; set; }
        
        [JsonProperty("backgroundImage")]
        public MetadataContent BackgroundImages { get; set; }
    }
}