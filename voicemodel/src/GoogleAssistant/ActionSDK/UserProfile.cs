using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK
{
    public class UserProfile
    {
        [JsonProperty("displayName")]
        public string DisplayName { get; set; }
        
        [JsonProperty("givenName")]
        public string FirstName { get; set; }
        
        [JsonProperty("familyName")]
        public string LastName { get; set; }
    }
}