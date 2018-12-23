using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK
{
    public class LongIntArgument : Argument
    {
        [JsonProperty("intValue")]
        public long IntValue { get; set; }
    }
}