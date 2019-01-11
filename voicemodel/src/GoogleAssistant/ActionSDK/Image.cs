using System;
using Newtonsoft.Json;
using VoiceBridge.Common;

namespace VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK
{
    public class Image
    {
        [JsonProperty("url")]
        public SecureUrl Url { get; set; }
        
        [JsonProperty("accessibilityText")]
        public string AccessibilityText { get; set; }
        
        [JsonProperty("height")]
        public int? Height { get; set; }
        
        [JsonProperty("width")]
        public int? Width { get; set; }
    }
}