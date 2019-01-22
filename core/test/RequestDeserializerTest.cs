using System.IO;
using VoiceBridge.Most.Test.TestData;
using VoiceBridge.Most.VoiceModel.Alexa;
using VoiceBridge.Most.VoiceModel.GoogleAssistant.DialogFlow;
using Xunit;

namespace VoiceBridge.Most.Test
{
    public class RequestDeserializerTest
    {
        [Fact]
        public void DeserializeAlexaRequest()
        {
            Assert.IsType<SkillRequest>(RequestDeserializer.Deserialize(Files.AlexaPlaybackFinished));
        }

        [Fact]
        public void DeserializeGoogleRequest()
        {
            Assert.IsType<AppRequest>(RequestDeserializer.Deserialize(Files.GoogleOptionSelected));
        }

        [Fact]
        public void IsAlexaRequestPositive()
        {
            Assert.True(new SkillRequest().IsAlexaRequest());
        }

        [Fact]
        public void IsAlexaRequestNegative()
        {
            Assert.False(new AppRequest().IsAlexaRequest());
        }
    }
}