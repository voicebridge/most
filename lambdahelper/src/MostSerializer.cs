using System;
using System.IO;
using System.Reflection;
using Amazon.Lambda.Core;
using Amazon.Lambda.Serialization.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace VoiceBridge.Most.LambdaHelper
{
  public class MostSerializer : ILambdaSerializer
  {
    private readonly JsonSerializer serializer;

    public MostSerializer()
    {
      this.serializer = JsonSerializer.Create(new JsonSerializerSettings());
      this.serializer.Converters.Add(new RequestJsonConverter());
    }

    private static bool DebugMode => Environment.GetEnvironmentVariable("MOST_DEBUG_ENABLED") == "1";

    public void Serialize<T>(T response, Stream responseStream)
    {
      try
      {
        if (DebugMode)
        {
          var json = JsonConvert.SerializeObject(response, Formatting.Indented);
          LambdaLogger.Log($"RESPONSE:\n{json}");
        }
        
        var streamWriter = new StreamWriter(responseStream);
        this.serializer.Serialize((TextWriter) streamWriter, (object) response);
        streamWriter.Flush();
      }
      catch (Exception ex)
      {
        throw new JsonSerializerException(
          $"Error converting the response object of type {(object) typeof(T).FullName} from the Lambda function to JSON: {(object) ex.Message}",
          ex);
      }
    }

    public T Deserialize<T>(Stream requestStream)
    {
      try
      {
        if (DebugMode)
        {
          var json = new StreamReader(requestStream).ReadToEnd();
          LambdaLogger.Log($"REQUEST:\n{json}");
          requestStream.Position = 0;
        }
        
        var reader = (TextReader) new StreamReader(requestStream);
        return this.serializer.Deserialize<T>((JsonReader) new JsonTextReader(reader));
      }
      catch (Exception ex)
      {
        var type = typeof(T);
        throw new JsonSerializerException(
          (object) type != (object) typeof(string)
            ? $"Error converting the Lambda event JSON payload to type {(object) type.FullName}: {(object) ex.Message}"
            : $"Error converting the Lambda event JSON payload to a string. JSON strings must be quoted, for example \"Hello World\" in order to be converted to a string: {(object) ex.Message}", ex);
      }
    }
  }
}
