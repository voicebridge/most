using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK
{
    public class SimpleResponseItem : RichResponseItem
    {
        [JsonProperty("simpleResponse")]
        public SimpleResponse Value { get; set; }
    }
}