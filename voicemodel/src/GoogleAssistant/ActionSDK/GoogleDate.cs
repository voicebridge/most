using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK
{
    public class GoogleDate
    {
        [JsonProperty("year")]
        public int Year { get; set; }
        
        [JsonProperty("month")]
        public int Month { get; set; }
        
        [JsonProperty("day")]
        public int Day { get; set; }
    }
}