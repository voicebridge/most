using System.Collections.Generic;
using VoiceBridge.Most.Alexa;
using VoiceBridge.Most.Google;
using VoiceBridge.Most.VoiceModel.Alexa;
using VoiceBridge.Most.VoiceModel.GoogleAssistant.DialogFlow;
using Xunit;

namespace VoiceBridge.Most.Test.Google
{
    public class GoogleCapabilitiesInputModelBuilderTest
    {
        [Fact]
        public void VerifyFullCapabilities()
        {
            var capabilities = RunSimulation(Files.GoogleImplicitRequestSample);
            Assert.Contains(DeviceCapability.Audio, capabilities);
            Assert.Contains(DeviceCapability.Display, capabilities);
            Assert.Contains(DeviceCapability.StreamMedia, capabilities);
        }
        
        private static IEnumerable<DeviceCapability> RunSimulation(string json)
        {
            var request = json.FromJson<AppRequest>();
            var context = new ConversationContext();
            var modelBuilder = new GoogleCapabilitiesInputModelBuilder();
            modelBuilder.Build(context, request);
            return context.Capabilities;
        }
    }
}