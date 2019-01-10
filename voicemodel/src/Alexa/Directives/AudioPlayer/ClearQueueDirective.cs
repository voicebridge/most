using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.Alexa.Directives.AudioPlayer
{
    public class ClearQueueDirective : DirectiveBase
    {
        public override string Type => AlexaConstants.AudioPlayer.Directives.ClearQueue;
        
        [JsonProperty("clearBehavior")]
        public string ClearBehavior { get; set; }
    }
}