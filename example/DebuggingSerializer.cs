using System.IO;
using Amazon.Lambda.Core;
using Amazon.Lambda.Serialization.Json;

namespace Sample
{
    public class DebuggingSerializer : ILambdaSerializer
    {
        private JsonSerializer serializer = new JsonSerializer();
        
        public T Deserialize<T>(Stream requestStream)
        {
            var end = new StreamReader(requestStream).ReadToEnd();
            LambdaLogger.Log(end);
            
            return serializer.Deserialize<T>(requestStream);
        }

        public void Serialize<T>(T response, Stream responseStream)
        {
            serializer.Serialize<T>(response, responseStream);
        }
    }
}