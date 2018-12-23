using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK
{
    public class Location
    {
        [JsonProperty("coordinates")]
        public GeoCoordinates Coordinates { get; set; }
        
        [JsonProperty("formattedAddress")]
        public string FormattedAddress { get; set; }
        
        [JsonProperty("zipCode")]
        public string ZipCode { get; set; }
        
        [JsonProperty("city")]
        public string City { get; set; }
        
        [JsonProperty("postalAddress")]
        public PostalAddress PostalAddress { get; set; }
        
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }
        
        [JsonProperty("notes")]
        public string Notes { get; set; }
        
        [JsonProperty("placeId")]
        public string PlaceId { get; set; }
    }
}