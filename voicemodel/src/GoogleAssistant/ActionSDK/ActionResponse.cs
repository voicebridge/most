using System.Collections.Generic;
using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK
{
    public class ActionResponse : IResponse
    {
        [JsonProperty("conversationToken")]
        public string ConversationToken { get; set; }
        
        [JsonProperty("userStorage")]
        public string UserStorage { get; set; }
        
        [JsonProperty("resetUserStorage")]
        public bool ResetUserStorage { get; set; }
        
        [JsonProperty("expectUserResponse")]
        public bool ExpectUserResponse { get; set; }
        
        [JsonProperty("isInSandbox")]
        public bool IsInSandbox { get; set; }

        [JsonProperty("expectedInputs")]
        public List<ExpectedInput> ExpectedInputs { get; set; }
        
        [JsonProperty("finalResponse")]
        public FinalResponse FinalResponse { get; set; }
        
        [JsonProperty("customPushMessage")]
        public CustomPushMessage CustomPushMessage { get; set; }
    }
}