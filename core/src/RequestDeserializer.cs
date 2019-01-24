using System;
using System.IO;
using Newtonsoft.Json;
using VoiceBridge.Most.VoiceModel;
using VoiceBridge.Most.VoiceModel.Alexa;

namespace VoiceBridge.Most
{
    public static class RequestDeserializer 
    {
        public static IRequest Deserialize(string json)
        {
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new RequestJsonConverter());
            return JsonConvert.DeserializeObject<IRequest>(json, settings) as IRequest;
        }

        public static bool IsAlexaRequest(this IRequest request)
        {
            return request is SkillRequest;
        }
    }
}