using VoiceBridge.Most.Alexa;
using VoiceBridge.Most.VoiceModel.Alexa;
using Xunit;

namespace VoiceBridge.Most.Test.Alexa
{
    public class AlexaResponseFactoryTest
    {
        [Fact]
        public void Create()
        {
            var context = new ConversationContext();
            var factory = new AlexaResponseFactory();
            var response = factory.Create(context);
            Assert.Equal(AlexaConstants.AlexaVersion, response.Version);
            Assert.NotNull(response.Content);
            Assert.NotNull(response.Content.Directives);
            Assert.NotNull(response.SessionAttributes);
        }
    }
}