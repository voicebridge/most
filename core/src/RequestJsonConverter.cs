using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using VoiceBridge.Most.VoiceModel;
using VoiceBridge.Most.VoiceModel.Alexa;
using VoiceBridge.Most.VoiceModel.GoogleAssistant.DialogFlow;

namespace VoiceBridge.Most
{
    public class RequestJsonConverter : JsonConverter
    {
        public override bool CanRead => true;

        public override bool CanWrite => false;
        
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jObject = JObject.Load(reader);
            var target = CreateInstance(jObject);
            if (target == null)
            {
                return null;
            }
            
            serializer.Populate(jObject.CreateReader(), target);
            return target;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(IRequest) ||
                   objectType == typeof(SkillRequest) || 
                   objectType == typeof(AppRequest);
        }

        private object CreateInstance(JObject obj)
        {
            if (obj.ContainsKey("queryResult"))
            {
                return new AppRequest();
            }

            return new SkillRequest();
        }
    }
}