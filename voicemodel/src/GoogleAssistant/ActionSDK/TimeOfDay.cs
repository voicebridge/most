using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK
{
    public class TimeOfDay
    {
        [JsonProperty("hours")]
        public int Hours { get; set; }
        
        [JsonProperty("minutes")]
        public int Minutes { get; set; }
        
        [JsonProperty("seconds")]
        public int Seconds { get; set; }
        
        [JsonProperty("nanos")]
        public int NanoSeconds { get; set; }
    }
}