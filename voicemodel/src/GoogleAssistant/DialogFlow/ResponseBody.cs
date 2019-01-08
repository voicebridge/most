using System;
using Newtonsoft.Json;
using VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK;

namespace VoiceBridge.Most.VoiceModel.GoogleAssistant.DialogFlow
{
    public class ResponseBody
    {
        private const int MaximumUserStorageSize = 10000;
        private string userStorage;

        [JsonProperty("expectUserResponse")]
        public bool ExpectUserResponse { get; set; } = false;

        [JsonProperty("userStorage")]
        public string UserStorage
        {
            get => userStorage;
            set
            {
                if (value?.Length > 10000)
                {
                    throw new InvalidOperationException("User storage has exceeded maximum size");
                }
                
                userStorage = value;
            }
        }

        [JsonProperty("richResponse")]
        public RichResponse RichResponse { get; set; }
    }
}