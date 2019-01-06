using System;
using System.Linq;
using System.Threading.Tasks;
using VoiceBridge.Most.Directives;
using VoiceBridge.Most.VoiceModel.Alexa.Directives;
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
        public void CanHandleIntentMatchButWrongType()
        {
            var intent = new IntentConfiguration("one");
            var model = new TestRequestModel {IntentName = "one"}.AsConversationContext();
            model.RequestType = RequestType.Launch;
            Assert.False(intent.CanHandle(model));       
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
        public async Task Do()
        {
            var dir = Util.QuickStub<IVirtualDirective>();
            var directive = await DynamicHandlerHelper.ExecuteHandle<IVirtualDirective>(intent => { intent.Do(c => dir); });
            Assert.Same(dir, directive);
        }
        
        [Fact]
        public async Task DoIgnoresOtherDirectives()
        {
            var dir = Util.QuickStub<IVirtualDirective>();
            var directive = await DynamicHandlerHelper.ExecuteHandle<IVirtualDirective>(intent =>
            {
                intent
                    .Say("I should not be executed!".AsPrompt())
                    .Do(context => dir);
            });
            
            Assert.Same(dir, directive);
        }
    }
}