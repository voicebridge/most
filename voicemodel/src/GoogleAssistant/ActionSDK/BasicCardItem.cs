using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK
{
    public class BasicCardItem : RichResponseItem
    {
        [JsonProperty("basicCard")]
        public BasicCard Value { get; set; }
    }
}