using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK
{
    public class Entitlement
    {
        [JsonProperty("sku")]
        public string SKU { get; set; }
        
        [JsonProperty("skuType")]
        public string SKUType { get; set; }
        
        [JsonProperty("inAppDetails")]
        public SignedData InAppDetails { get; set; }
    }
}