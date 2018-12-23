using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK
{
    public class BasicCardItem
    {
        [JsonProperty("basicCard")]
        public BasicCard Value { get; set; }
    }
}