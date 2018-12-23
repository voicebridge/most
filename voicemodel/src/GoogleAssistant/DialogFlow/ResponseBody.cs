using Newtonsoft.Json;
using VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK;

namespace VoiceBridge.Most.VoiceModel.GoogleAssistant.DialogFlow
{
    public class ResponseBody
    {
        [JsonProperty("expectUserResponse")]
        public bool ExpectUserResponse { get; set; }

        [JsonProperty("richResponse")]
        public RichResponse RichResponse { get; set; }
    }
}