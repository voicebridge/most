using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.Alexa
{
    public class PlainTextOutputSpeech : IOutputSpeech
    {
        [JsonProperty("type")]
        public string Type => AlexaConstants.OutputType.PlainText;

        [JsonProperty("text")]
        public string Text {get; set;}

        public static IOutputSpeech Create(string text)
        {
            return new PlainTextOutputSpeech {Text = text};
        }
    }
}