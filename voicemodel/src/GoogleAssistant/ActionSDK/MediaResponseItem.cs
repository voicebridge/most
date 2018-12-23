using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK
{
    public class MediaResponseItem : RichResponseItem
    {
        [JsonProperty("mediaResponse")]
        public MediaResponse Value { get; set; }
    }
}