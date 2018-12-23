using VoiceBridge.Most.VoiceModel.Alexa;
using Xunit;

namespace VoiceBridge.Most.Test
{
    public class CompositeResponseBuilderTest
    {
        [Fact]
        public void HappyPath()
        {
            var response = new SkillResponse();
            var response2 = new SkillResponse();
            var context = new ConversationContext();
            var context2 = new ConversationContext();

            var builder1 = Util.CreateResponseBuilder(context, response);
            var builder2 = Util.CreateResponseBuilder(context2, response2);
            var builder3 = Util.CreateResponseBuilder(context, response2);
            var builders = Util.ToEnumerable(builder1, builder2, builder3);
            
            var composite = new CompositeResponseBuilder<SkillResponse>(builders);
            Assert.True(composite.CanHandle(context));
            Assert.Same(response, composite.Build(context));          
        }
        
        [Fact]
        public void NoSuitableBuilder()
        {
            var response = new SkillResponse();
            var response2 = new SkillResponse();
            var context = new ConversationContext();
            var context2 = new ConversationContext();

            var builder1 = Util.CreateResponseBuilder(context, response);
            var builder2 = Util.CreateResponseBuilder(context, response2);
            var builders = Util.ToEnumerable(builder1, builder2);
            
            var composite = new CompositeResponseBuilder<SkillResponse>(builders);
            Assert.Null(composite.Build(context2));          
        }
    }
}