using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.Alexa
{
    public class SSMLOutputSpeech : IOutputSpeech
    {
        [JsonProperty("type")]
        public string Type => AlexaConstants.OutputType.SSMLSpeech;
        
        [JsonProperty("ssml")]
        public string Content { get; set; }
    }
}