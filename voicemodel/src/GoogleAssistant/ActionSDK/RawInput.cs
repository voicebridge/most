using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK
{
    public abstract class RawInput
    {
        [JsonProperty("inputType")]
        public string InputType { get; set; }
    }
}