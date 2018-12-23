using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.Alexa
{
    public class Permissions
    {
        [JsonProperty("consentToken")]
        public string ConsentToken { get; set; }
    }
}