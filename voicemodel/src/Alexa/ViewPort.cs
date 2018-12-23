using System.Collections.Generic;
using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.Alexa
{
    public class ViewPort
    {
        [JsonProperty("experiences")]
        public ViewPortExperience[] Experiences { get; set; }
        
        [JsonProperty("shape")]
        public string Shape { get; set; }
        
        [JsonProperty("pixelWidth")]
        public int PixelWidth { get; set; }
        
        [JsonProperty("pixelHeight")]
        public int PixelHeight { get; set; }
        
        [JsonProperty("dpi")]
        public int DPI { get; set; }
        
        [JsonProperty("currentPixelWidth")]
        public int CurrentPixelWidth { get; set; }
        
        [JsonProperty("currentPixelHeight")]
        public int CurrentPixelHeight { get; set; }
        
        [JsonProperty("touch")]
        public string[] TouchModes { get; set; }
    }
}