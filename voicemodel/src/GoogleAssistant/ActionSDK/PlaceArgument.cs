using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK
{
    public class PlaceArgument : Argument
    {
        [JsonProperty("placeValue")]
        public Location PlaceValue { get; set; }
    }
}