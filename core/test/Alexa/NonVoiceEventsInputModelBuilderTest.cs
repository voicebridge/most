using System.Linq;
using VoiceBridge.Most.Alexa;
using VoiceBridge.Most.VoiceModel.Alexa;
using Xunit;

namespace VoiceBridge.Most.Test.Alexa
{
    public class NonVoiceEventsInputModelBuilderTest
    {
        [Fact]
        public void HandleAPLUserInput()
        {
            var request = Files.APLUserEventRequest.FromJson<SkillRequest>();
            var context = new ConversationContext();
            var builder = new NonVoiceEventsInputModelBuilder();
            builder.Build(context, request);
            var input = context.Extensions.Get<NonVoiceInput>();
            Assert.Equal("myTouchWrapper", input.SourceName);
            Assert.Equal("orderDrink", input.Values.Single());
        }

        [Fact]
        public void HandleDisplayElementEvent()
        {
            var request = Files.AlexaDisplayElementSelected.FromJson<SkillRequest>();
            var context = new ConversationContext();
            var builder = new NonVoiceEventsInputModelBuilder();
            builder.Build(context, request);
            var input = context.Extensions.Get<NonVoiceInput>();
            Assert.Equal("my-element", input.SourceName);
            Assert.Empty(input.Values);
        }
    }
}