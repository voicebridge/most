using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace VoiceBridge.Most.VoiceModel.Alexa.Directives
{
    public class PlayAudioDirective : DirectiveBase
    {
        public override string Type => AlexaConstants.AudioPlayer.Directives.Play;
        
        [JsonProperty("playBehavior")]
        public string PlayBehavior { get; set; }
        
        [JsonProperty("audioItem")]
        public AudioItem Audio { get; set; }
    }
}