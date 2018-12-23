using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK
{
    public class FloatArgument : Argument
    {
        [JsonProperty("floatValue")]
        public float FloatValue { get; set; }
    }
}