using System;
using Newtonsoft.Json;
using VoiceBridge.Common;

namespace VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK
{
    public abstract class MediaObject
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("description")]
        public string Description { get; set; }
        
        [JsonProperty("contentUrl")]
        public SecureUri ContentUrl { get; set; }
    }
}