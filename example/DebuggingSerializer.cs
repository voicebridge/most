using System.IO;
using Amazon.Lambda.Core;
using Newtonsoft.Json;
using VoiceBridge.Most;
using JsonSerializer = Amazon.Lambda.Serialization.Json.JsonSerializer;

namespace Sample
{
    public class DebuggingSerializer : Amazon.Lambda.Serialization.Json.JsonSerializer
    {
        private JsonSerializer serializer = new JsonSerializer();

        public DebuggingSerializer() : base(new[]
        {
            new RequestJsonConverter()
        })
        {
        }
    }
}