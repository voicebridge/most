using System.Collections.Generic;
using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.Alexa
{
    public class DeviceInfo
    {
        [JsonProperty("supportedInterfaces")]
        public Dictionary<string, object> SupportedInterfaces {get; set;}

        [JsonProperty("deviceId")]
        public string DeviceId {get; set;}
    }
}