using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK
{
    public class BoolArgument : Argument
    {
        [JsonProperty("boolValue")]
        public bool? BoolValue { get; set; }
    }
}