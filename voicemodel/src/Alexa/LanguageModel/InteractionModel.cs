using System.Collections.Generic;
using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.Alexa.LanguageModel
{
    public class InteractionModel
    {
        public InteractionModel()
        {
            this.LanguageModel = new LanguageModel();
            this.Dialog = new Dialog();
            this.Prompts = new List<PromptDefinition>();
        }
        
        [JsonProperty("languageModel")]
        public LanguageModel LanguageModel { get; }
        
        [JsonProperty("dialog")]
        public Dialog Dialog { get; }
        
        [JsonProperty("prompts")]
        public List<PromptDefinition> Prompts { get; }
    }
}