using System.Collections.Generic;
using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK
{
    public class MediaResponse
    {
        [JsonProperty("mediaType")]
        public string MediaType { get; set; }
        
        [JsonProperty("mediaObjects")]
        public List<MediaObject> MediaObjects { get; set; }
    }
}