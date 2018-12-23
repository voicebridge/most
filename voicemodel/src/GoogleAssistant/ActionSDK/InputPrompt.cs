using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK
{
    public class InputPrompt
    {
        [JsonProperty("richInitialPrompt")]
        public RichResponse RichInitialPrompt { get; set; }
        
        [JsonProperty("noInputPrompts")]
        public SimpleResponse[] NoInputPrompts { get; set; }
    }
}