using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK
{
    public class SimpleResponse
    {
        [JsonProperty("textToSpeech")]
        public string TextToSpeech { get; set; }
        
        [JsonProperty("ssml")]
        public string SSML { get; set; }
        
        [JsonProperty("displayText")]
        public string DisplayText { get; set; }
    }
}