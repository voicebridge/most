using System.Collections.Generic;
using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.Alexa.LanguageModel
{
    public class DialogIntentSetting
    {
        public DialogIntentSetting()
        {
            this.Prompts = new Empty();
            this.Slots = new List<DialogIntentSlotSetting>();
        }
        
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("confirmationRequired")]
        public bool ConfirmationRequired { get; set; }
        
        [JsonProperty("prompts")]
        private Empty Prompts { get; }
        
        [JsonProperty("slots")]
        public List<DialogIntentSlotSetting> Slots { get; }
    }
}