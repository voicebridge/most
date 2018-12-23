using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK
{
    public class PostalAddress
    {
        [JsonProperty("revision")]
        public string Revision { get; set; }
        
        [JsonProperty("regionCode")]
        public string RegionCode { get; set; }
        
        [JsonProperty("postalCode")]
        public string PostalCode { get; set; }
        
        [JsonProperty("sortingCode")]
        public string SortingCode { get; set; }
        
        [JsonProperty("administrativeArea")]
        public string AdministrativeArea { get; set; }
        
        [JsonProperty("locality")]
        public string Locality { get; set; }
        
        [JsonProperty("sublocality")]
        public string SubLocality { get; set; }
        
        [JsonProperty("addressLines")]
        public string[] AddressLines { get; set; }
        
        [JsonProperty("recipients")]
        public string[] Recipients { get; set; }
        
        [JsonProperty("organization")]
        public string Organization { get; set; }
    }
}