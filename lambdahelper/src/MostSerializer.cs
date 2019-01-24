using System;
using System.IO;
using Amazon.Lambda.Core;
using Amazon.Lambda.Serialization.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace VoiceBridge.Most.LambdaHelper
{
  public class MostSerializer : Amazon.Lambda.Serialization.Json.JsonSerializer
  {
    public MostSerializer() : base(new []{new RequestJsonConverter() })
    {
    }
  }
}
