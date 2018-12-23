using System;
using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK
{
    public class UserInfo
    {
        [Obsolete("Google has deprecated this property")]
        [JsonProperty("userId")]
        public string UserId { get; set; }
        
        [JsonProperty("idToken")]
        public string IdToken { get; set; }
        
        [JsonProperty("accessToken")]
        public string AccessToken { get; set; }
        
        [JsonProperty("permissions")]
        public string[] Permissions { get; set; }
        
        [JsonProperty("locale")]
        public string Locale { get; set; }
        
        [JsonProperty("lastSeen")]
        public string LastSeen { get; set; }
        
        [JsonProperty("userStorage")]
        public string UserStorage { get; set; }
        
        [JsonProperty("profile")]
        public UserProfile Profile { get; set; }
        
        [JsonProperty("packageEntitlements")]
        public PackageEntitlement[] PackageEntitlements { get; set; }
    }
}