using System.Collections.Generic;
using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK
{
    public class ActionRequest : IRequest
    {
        [JsonProperty("user")]
        public UserInfo User { get; set; }
        
        [JsonProperty("device")]
        public Device Device { get; set; }
        
        [JsonProperty("surface")]
        public Surface Surface { get; set; }
        
        [JsonProperty("conversation")]
        public Conversation Conversation { get; set; }
        
        [JsonProperty("isInSandbox")]
        public bool IsInSandbox { get; set; }
        
        [JsonProperty("availableSurfaces")]
        
        public List<Surface> AvailableSurfaces { get; set; }
        
        [JsonProperty("inputs")]
        public List<Input> Inputs { get; set; }
    }
}