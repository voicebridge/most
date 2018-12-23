using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK
{
    public class Cell
    {
       [JsonProperty("text")]
       public string Text { get; set; }
    }
}