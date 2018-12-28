using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.Alexa.LanguageModel
{
    public class DialogIntentSlotSetting
    {
        public DialogIntentSlotSetting()
        {
            this.Prompts = new DialogIntentPromptIdSettings();
        }
        
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("type")]
        public string Type { get; set; }
        
        [JsonProperty("confirmationRequired")]
        public bool ConfirmationRequired { get; set; }
        
        [JsonProperty("elicitationRequired")]
        public bool ElicitationRequired { get; set; }
        
        [JsonProperty("prompts")]
        public DialogIntentPromptIdSettings Prompts { get; }
    }
}