using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK
{
    public class AndroidApp
    {
        [JsonProperty("packageName")]
        public string PackageName { get; set; }
        
        [JsonProperty("versions")]
        public VersionFilter[] Versions { get; set; }
    }
}