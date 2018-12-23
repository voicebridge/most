using Xunit;
using Xunit.Abstractions;

namespace VoiceBridge.Most.Test
{
    public class CompositeHandlerTest : TestBase
    {
        [Fact]
        public void CanHandleReturnsTrueIfAnyCanHandle()
        {
            var context = new ConversationContext {RequestModel = new TestRequestModel()};
            var context2 = new ConversationContext {RequestModel = new TestRequestModel()};
            
            var handler1 = Util.CreateHandler(context2);
            var handler2 = Util.CreateHandler(context);
            var handler3 = Util.CreateHandler(context2);
            
            var composite = new CompositeHandler(new[] {handler1.Object, handler2.Object, handler3.Object}, this);
            Assert.True(composite.CanHandle(context));
        }
        
        [Fact]
        public void CanHandleReturnsNoSuitableHandlers()
        {
            var context = new ConversationContext {RequestModel = new TestRequestModel()};
            var context2 = new ConversationContext {RequestModel = new TestRequestModel()};
            
            var handler1 = Util.CreateHandler(context2);
            var handler2 = Util.CreateHandler(context2);
            
            var composite = new CompositeHandler(new[] {handler1.Object, handler2.Object}, this);
            Assert.False(composite.CanHandle(context));
        }
        
        [Fact]
        public async void HandleAllSuitableAreTriggered()
        {
            var context = new ConversationContext {RequestModel = new TestRequestModel()};
            var context2 = new ConversationContext {RequestModel = new TestRequestModel()};
            
            var handler1 = Util.CreateHandler(context2);
            var handler2 = Util.CreateHandler(context);
            var handler3 = Util.CreateHandler(context2);
            
            var composite = new CompositeHandler(new[] {handler1.Object, handler2.Object, handler3.Object}, this);
            await composite.Handle(context2);
            handler1.Verify(x => x.Handle(context2));
            handler3.Verify(x => x.Handle(context2));
            handler2.Verify(x => x.Handle(context2), Moq.Times.Never);
        }

        public CompositeHandlerTest(ITestOutputHelper output) : base(output)
        {
        }
    }
}