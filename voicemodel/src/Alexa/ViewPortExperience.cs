using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.Alexa
{
    public class ViewPortExperience
    {
        [JsonProperty("arcMinuteWidth")]
        public int ArcMinuteWidth { get; set; }
        
        [JsonProperty("arcMinuteHeight")]
        public int ArcMinuteHeight { get; set; }
        
        [JsonProperty("canRotate")]
        public bool CanRotate { get; set; }
        
        [JsonProperty("canResize")]
        public bool CanResize { get; set; }
    }
}