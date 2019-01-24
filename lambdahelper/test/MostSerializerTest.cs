using System;
using System.Reflection;
using VoiceBridge.Common.Extensions;
using VoiceBridge.Most.VoiceModel;
using Xunit;

namespace VoiceBridge.Most.LambdaHelper.Test
{
    public class MostSerializerTest
    {
        [Fact]
        public void Ctor()
        {
            var serializer = new MostSerializer();
        }
        
        [Fact]
        public void Deserialize()
        {
            var json = Assembly.GetExecutingAssembly()
                .GetManifestResourceStream("VoiceBridge.Most.LambdaHelper.Test.SampleAlexaRequest.json");
            var serializer = new MostSerializer();
            Assert.NotNull(serializer.Deserialize<IRequest>(json));
        }
    }
}