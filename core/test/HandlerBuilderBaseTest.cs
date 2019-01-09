using System.Threading.Tasks;
using VoiceBridge.Most.VoiceModel.Alexa;
using Xunit;

namespace VoiceBridge.Most.Test
{
    public class HandlerBuilderBaseTest
    {
        [Fact]
        public void CanHandleChecksRequestType()
        {
            var context = new ConversationContext {RequestType = RequestType.Launch};
            var handler = new HandlerBuilderTester(RequestType.Intent);
            Assert.False(handler.CanHandle(context));
        }

        [Fact]
        public void AllConditionsMustBeTruePositiveCase()
        {
            var context = new ConversationContext {RequestType = RequestType.Launch};
            var handler = new HandlerBuilderTester(RequestType.Launch);
            handler.When(c => c == context);
            handler.When(c => c.RequestType == RequestType.Launch);
            Assert.True(handler.CanHandle(context));
        }
        
        [Fact]
        public void AllConditionsMustBeTrueNegativeCase()
        {
            var context = new ConversationContext {RequestType = RequestType.Launch};
            var handler = new HandlerBuilderTester(RequestType.Launch);
            handler.When(c => c == context);
            handler.When(c => false);
            Assert.False(handler.CanHandle(context));
        }

        [Fact]
        public async Task WhenConditionsAreTrueActionsAreExecuted()
        {
            var action1Result = Util.QuickStub<IVirtualDirective>();
            var action2Result = Util.QuickStub<IVirtualDirective>();
            var context = new ConversationContext {RequestType = RequestType.Launch};
            var handler = new HandlerBuilderTester(RequestType.Launch);
            handler
                .When(c => true)
                .Do(x => action1Result)
                .Do(x => action2Result);
            await handler.Handle(context);
            Assert.Contains(action1Result, context.OutputDirectives);
            Assert.Contains(action2Result, context.OutputDirectives);
        }
        
        [Fact]
        public async Task NoActionIsExecutedWhenAnyConditionIsFalse()
        {
            var action1Result = Util.QuickStub<IVirtualDirective>();
            var context = new ConversationContext {RequestType = RequestType.Launch};
            var handler = new HandlerBuilderTester(RequestType.Launch);
            handler
                .When(c => false)
                .Do(x => action1Result);
            await handler.Handle(context);
            Assert.Empty(context.OutputDirectives);
        }

        [Fact]
        public void RequestTypeMatchesValuePassedIn()
        {
            var handler = new HandlerBuilderTester(RequestType.Intent);
            Assert.Equal(RequestType.Intent, handler.TypeOfRequestToMatch);
        }
        
        private class HandlerBuilderTester : RequestHandlerBuilder
        {
            public HandlerBuilderTester(RequestType targetRequestType) : base(targetRequestType)
            {
            }
        }
    }
}