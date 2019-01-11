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
            Assert.Null(response.Content.ShouldEndSession);
        }

        [Fact]
        public void SessionIsTransferred()
        {
            var context = new ConversationContext();
            context.SessionValues["s1"] = "v1";
            context.SessionValues["s2"] = "v2";
            var factory = new AlexaResponseFactory();
            var response = factory.Create(context);
            Assert.Equal("v1", response.SessionAttributes["s1"]);
            Assert.Equal("v2", response.SessionAttributes["s2"]);
        }
    }
}