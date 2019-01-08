using VoiceBridge.Most.Google;
using VoiceBridge.Most.VoiceModel.GoogleAssistant.DialogFlow;
using Xunit;

namespace VoiceBridge.Most.Test.Google
{
    public class NonVoiceInputModelBuilderTest
    {
        [Fact]
        public void InputIsReadCorrectly()
        {
            var request = Files.GoogleOptionSelected.FromJson<AppRequest>();
            var context = new ConversationContext();
            new NonVoiceInputModelBuilder().Build(context, request);
            var input = context.Extensions.Get<NonVoiceInput>();
            Assert.Equal("Key of selected item", input.SourceName);
            Assert.Empty(input.Values);
        }
    }
}