using System;
using Newtonsoft.Json;
using VoiceBridge.Common;

namespace VoiceBridge.Most.VoiceModel.Alexa
{
    public class AlexaStream
    {
        [JsonProperty("url")]
        public SecureUrl Stream { get; set; }
        
        [JsonProperty("token")]
        public string Token { get; set; }
        
        [JsonProperty("expectedPreviousToken")]
        public string ExpectedPreviousToken { get; set; }
        
        [JsonProperty("offsetInMilliseconds")]
        public int OffsetInMilliseconds { get; set; }
    }
}