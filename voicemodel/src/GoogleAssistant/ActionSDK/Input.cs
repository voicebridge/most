using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK
{
    public class Input
    {
        [JsonProperty("rawInputs", ItemConverterType = typeof(GoogleAssistantJsonConverter<RawInput>))]
        public RawInput[] RawInputs { get; set; }
        
        [JsonProperty("intent")]
        public string Intent { get; set; }
        
        [JsonProperty("arguments", ItemConverterType = typeof(GoogleAssistantJsonConverter<Argument>))]
        public Argument[] Arguments { get; set; }
    }
}