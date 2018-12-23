using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK
{
    public class PackageEntitlement
    {
        [JsonProperty("packageName")]
        public string PackageName { get; set; }
        
        [JsonProperty("entitlements")]
        public Entitlement[] Entitlements { get; set; }
    }
}