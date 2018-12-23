using System.Collections.Generic;
using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK
{
    public class RichResponse
    {
        [JsonProperty("items", ItemConverterType = typeof(GoogleAssistantJsonConverter<RichResponseItem>))]
        public List<RichResponseItem> Items { get; set; }
        
        [JsonProperty("suggestions")]
        public Suggestion[] Suggestions { get; set; }
        
        [JsonProperty("linkOutSuggestion")]
        public LinkOutSuggestion LinkOutSuggestion { get; set; }
    }
}