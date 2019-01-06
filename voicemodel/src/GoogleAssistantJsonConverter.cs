using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using VoiceBridge.Most.VoiceModel.GoogleAssistant;
using VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK;

namespace VoiceBridge.Most.VoiceModel
{
    public class GoogleAssistantJsonConverter<TBase> : JsonConverter
    {
        private readonly Dictionary<string, Type> mapping = LoadMapping();
        
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
            return typeof(TBase).IsAssignableFrom(objectType);
        }

        public override bool CanRead => true;

        public override bool CanWrite => false;

        private object CreateInstance(JObject obj)
        {
            foreach (var key in this.mapping.Keys)
            {
                if (obj.ContainsKey(key))
                {
                    return Activator.CreateInstance(this.mapping[key]);
                }
            }

            if (typeof(TBase).IsAssignableFrom(typeof(Argument)))
            {
                return new TextOnlyArgument();
            }
            
            return null;
        }

        private static Dictionary<string, Type> LoadMapping()
        {
            var mapping = new Dictionary<string, Type>
            {
                ["simpleResponse"] = typeof(SimpleResponseItem),
                ["intValue"] = typeof(LongIntArgument),
                ["floatValue"] = typeof(FloatArgument),
                ["boolValue"] = typeof(BoolArgument),
                ["datetimeValue"] = typeof(DateTimeArgument),
                ["placeValue"] = typeof(PlaceArgument),
                ["query"] = typeof(QueryInput),
                ["url"] = typeof(UrlInput)
            };
            return mapping;
        }
    }
}