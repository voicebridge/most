using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK
{
    public class VersionFilter
    {
        [JsonProperty("minVersion")]
        public int MinimumVersion { get; set; }
        
        [JsonProperty("maxVersion")]
        public int MaximumVersion { get; set; }
    }
}