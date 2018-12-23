using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.Alexa
{
    public class UserInfo
    {
        [JsonProperty ("userId")]
        public string UserId {get; set;}

        [JsonProperty("accessToken")]
        public string AccessToken {get; set;}

        [JsonProperty("permissions")]
        public Permissions Permissions { get; set; }
    }
}