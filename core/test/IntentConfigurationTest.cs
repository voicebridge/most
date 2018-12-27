using System;
using System.Linq;
using System.Threading.Tasks;
using VoiceBridge.Most.Directives;
using Xunit;

namespace VoiceBridge.Most.Test
{
    public class IntentConfigurationTest
    {
        [Fact]
        public void ThrowsIfIntentTypeIsPassedIn()
        {
            Assert.Throws<ArgumentException>(() => new IntentConfiguration(RequestType.Intent));
        }
        
        [Fact]
        public void CanHandleMatchedType()
        {
            var intent = new IntentConfiguration(RequestType.Launch);
            var context = new TestRequestModel {IntentName = "one"}.AsConversationContext();
            context.RequestType = RequestType.Launch;
            Assert.True(intent.CanHandle(context));       
        }
        
        [Fact]
        public void CanHandleTypeDoesNotMatch()
        {
            var intent = new IntentConfiguration(RequestType.Error);
            var context = new TestRequestModel {IntentName = "one"}.AsConversationContext();
            context.RequestType = RequestType.Launch;
            Assert.False(intent.CanHandle(context));       
        }
        
        [Fact]
        public void CanHandleIntentMatch()
        {
            var intent = new IntentConfiguration("one");
            var model = new TestRequestModel {IntentName = "one"};
            Assert.True(intent.CanHandle(model.AsConversationContext()));       
        }
        
        [Fact]
        public void CanHandleIntentOneConditionMatch()
        {
            var intent =
                new IntentConfiguration("one")
                    .When(x => x.RequestModel.ParameterHasValue("p1"));
            
            var model = new TestRequestModel
            {
                IntentName = "one"
            };
            
            model.Parameters["p1"] = "1".ToParameterValue();
            
            Assert.True(intent.CanHandle(model.AsConversationContext()));       
        }

        [Fact]
        public void CanHandleIntentDoesNotMatch()
        {
            var intent = new IntentConfiguration("one");
            var model = new TestRequestModel {IntentName = "two"};
            Assert.False(intent.CanHandle(model.AsConversationContext())); 
        }
        
        [Fact]
        public void CanHandleIntentMatchingIsCaseInsensitive()
        {
            var intent = new IntentConfiguration("ONE");
            var model = new TestRequestModel {IntentName = "one"};
            Assert.True(intent.CanHandle(model.AsConversationContext())); 
        }
        
        [Fact]
        public void CanHandleIntentOneConditionNoMatch()
        {
            var intent =
                new IntentConfiguration("one")
                    .When(x => x.RequestModel.GetParameterValue("p1") == "2");
            
            var model = new TestRequestModel
            {
                IntentName = "one"
            };
            
            model.Parameters["p1"] = "1".ToParameterValue();
            
            Assert.False(intent.CanHandle(model.AsConversationContext()));       
        }

        [Fact]
        public async Task HandleSay()
        {
            var directive = await ExecuteHandle<SayDirective>(i => i.Say("hello"));
            Assert.Equal(directive.Prompt.Content, "hello");
            Assert.False(directive.Prompt.IsSSML);
        }
        
        [Fact]
        public async Task HandleSayPrompt()
        {
            var prompt = new Prompt();
            var directive = await ExecuteHandle<SayDirective>(i => i.Say(prompt));
            Assert.Same(prompt, directive.Prompt);
        }
        
        [Fact]
        public async Task HandleAsk()
        {
            const string parameterName = "a";
            const string text = "b";
            var directive = await ExecuteHandle<AskForValueDirective>(i => i.AskFor(parameterName, text));
            Assert.Equal(directive.Prompt.Content, text);
            Assert.False(directive.Prompt.IsSSML);
            Assert.Equal(parameterName, directive.ParameterName);
        }
        
        [Fact]
        public async Task HandleAskWithPrompt()
        {
            const string parameterName = "a";
            var prompt = new Prompt();
            var directive = await ExecuteHandle<AskForValueDirective>(i => i.AskFor(parameterName, prompt));
            Assert.Same(prompt, directive.Prompt);
            Assert.Equal(parameterName, directive.ParameterName);
        }

        [Fact]
        public async Task Do()
        {
            var dir = Util.QuickStub<IVirtualDirective>();
            var directive = await ExecuteHandle<IVirtualDirective>(intent => { intent.Do(() => dir); });
            Assert.Same(dir, directive);
        }
        
        [Fact]
        public async Task DoIgnoresOtherDirectives()
        {
            var dir = Util.QuickStub<IVirtualDirective>();
            var directive = await ExecuteHandle<IVirtualDirective>(intent =>
            {
                intent
                    .Say("I should not be executed!")
                    .Do(() => dir);
            });
            
            Assert.Same(dir, directive);
        }

        private static async Task<TDirective> ExecuteHandle<TDirective>(Action<IntentConfiguration> action)
            where TDirective : IVirtualDirective
        {
            var context = new ConversationContext
            {
                RequestModel = new TestRequestModel
                {
                    IntentName = "one"
                },
                RequestType = RequestType.Intent
            };
            var intent =new IntentConfiguration(context.RequestModel.IntentName);
            action(intent);
            await intent.Handle(context);
            return (TDirective)context.OutputDirectives.Single();
        }
    }
}