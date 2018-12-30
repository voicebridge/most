using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.Alexa.Directives
{
    public abstract class DirectiveBase : IAlexaDirective
    {
        [JsonProperty("type")]
        public abstract string Type { get; }
    }
}