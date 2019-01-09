using System.Collections.Generic;
using VoiceBridge.Most.Alexa;
using VoiceBridge.Most.VoiceModel.Alexa;
using VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK;
using Xunit;

namespace VoiceBridge.Most.Test.Alexa
{
    public class AlexaCapabilitiesInputModelBuilderTest
    {
        [Fact]
        public void TestForAllCapabilities()
        {
            var list = RunSimulation(Files.APLUserEventRequest);
            Assert.Contains(DeviceCapability.Audio, list);
            Assert.Contains(DeviceCapability.Display, list);
            Assert.Contains(DeviceCapability.StreamMedia, list);
            Assert.Contains(DeviceCapability.AlexaPresentationLanguage, list);
            
        }

        private static IEnumerable<DeviceCapability> RunSimulation(string json)
        {
            var request = json.FromJson<SkillRequest>();
            var context = new ConversationContext();
            var modelBuilder = new AlexaCapabilitiesInputModelBuilder();
            modelBuilder.Build(context, request);
            return context.Capabilities;
        }
    }
}