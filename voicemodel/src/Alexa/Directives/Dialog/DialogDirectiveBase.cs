using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.Alexa.Directives.Dialog
{
    public abstract class DialogDirectiveBase : DirectiveBase
    {
        [JsonProperty("updatedIntent", NullValueHandling = NullValueHandling.Ignore)] 
        public Intent UpdatedIntent { get; set; }
    }
}