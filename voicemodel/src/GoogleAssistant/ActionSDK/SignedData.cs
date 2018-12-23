using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK
{
    public class SignedData
    {
        [JsonProperty("inAppPurchaseData")]
        public string InAppPurchaseData { get; set; }
        
        [JsonProperty("inAppPurchaseSignature")]
        public string InAppPurchaseSignature { get; set; }
    }
}