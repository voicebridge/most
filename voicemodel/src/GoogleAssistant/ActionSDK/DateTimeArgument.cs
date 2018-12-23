using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK
{
    public class DateTimeArgument : Argument
    {
        [JsonProperty("dateTimeValue")]
        public GoogleDateTime DateTimeValue { get; set; }
    }
}