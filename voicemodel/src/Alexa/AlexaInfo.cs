using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.Alexa
{
    public class AlexaInfo
    {
        [JsonProperty("user")]
        public UserInfo User {get; set;}

        [JsonProperty("device")]
        public DeviceInfo Device {get; set;}
        
        [JsonProperty("application")]
        public ApplicationInfo Application { get; set; }

        [JsonProperty("apiEndpoint")]
        public string ApiEndpoint {get; set;}
        
        [JsonProperty("apiAccessToken")]
        public string ApiAccessToken { get; set; }
        
        [JsonProperty("ViewPort")]
        public ViewPort ViewPort { get; set; }
    }
}