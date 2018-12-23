using System.Collections.Generic;
using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK
{
    public class ExpectedInput
    {
        [JsonProperty("inputPrompt")]
        public InputPrompt InputPrompt { get; set; }
        
        [JsonProperty("possibleIntents")]
        public List<ExpectedIntent> PossibleIntents { get; set; }
        
        [JsonProperty("speechBiasingHints")]
        public List<string> SpeechBiasingHints { get; set; }
    }
}