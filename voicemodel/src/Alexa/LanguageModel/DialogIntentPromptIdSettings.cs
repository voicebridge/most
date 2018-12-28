using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.Alexa.LanguageModel
{
    public class DialogIntentPromptIdSettings
    {
        [JsonProperty("elicitation")]
        public string ElicitationPromptId { get; set; }
    }
}