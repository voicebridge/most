using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.Alexa.Directives
{
    public abstract class DialogDirectiveBase : IAlexaDirective
    {

        [JsonProperty("type")] 
        public abstract string Type { get; }

        [JsonProperty("updatedIntent", NullValueHandling = NullValueHandling.Ignore)] 
        public Intent UpdatedIntent { get; set; }
    }
}