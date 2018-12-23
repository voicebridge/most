using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK
{
    public class MediaResponse
    {
        [JsonProperty("mediaType")]
        public string MediaType { get; set; }
        
        [JsonProperty("mediaObjects")]
        public MediaObject[] MediaObjects { get; set; }
    }
}