using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK
{
    public class GeoCoordinates
    {
        [JsonProperty("latitude")]
        public decimal LatitudeInDegrees { get; set; }
        
        [JsonProperty("longitude")]
        public decimal LongitudeInDegrees { get; set; }
    }
}