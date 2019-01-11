using VoiceBridge.Most.VoiceModel.Alexa;
using Xunit;
using Xunit.Abstractions;

namespace VoiceBridge.Most.Test
{
    public class CompositeInputModelBuilderTest : TestBase
    {
        [Fact]
        public void HappyPath()
        {
            var request = new SkillRequest();
            var context = new ConversationContext();

            var builder1 = Util.CreateInputModelBuilder(context, request);
            var builder2 = Util.CreateInputModelBuilder(context, request);
            var builders = Util.ToEnumerable(builder1, builder2);
            var composite = new CompositeInputModelBuilder<SkillRequest>(builders, this);
            composite.Build(context, request);
            builder1.Verify(x => x.Build(context, request));
            builder2.Verify(x => x.Build(context, request));
        }

        public CompositeInputModelBuilderTest(ITestOutputHelper output) : base(output)
        {
        }
    }
}