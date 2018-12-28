using System.Collections.Generic;
using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.Alexa.LanguageModel
{
    public class Dialog
    {
        public Dialog()
        {
            this.Intents = new List<DialogIntentSetting>();
        }
        
        [JsonProperty("intents")]
        public List<DialogIntentSetting> Intents { get; }
    }
}