using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.Alexa.LanguageModel
{
    public class ModelDocument
    {
        public ModelDocument()
        {
            this.Model = new InteractionModel();
        }
        
        [JsonProperty("interactionModel")]
        public InteractionModel Model { get; }
    }
}