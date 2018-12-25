using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using VoiceBridge.Most.Alexa;
using VoiceBridge.Most.Directives;
using VoiceBridge.Most.Test.TestData;
using VoiceBridge.Most.VoiceModel.Alexa;
using Xunit;
using Xunit.Abstractions;

namespace VoiceBridge.Most.Test
{
    public class ConversionEngineTest : TestBase
    {
        private readonly string requestId = Generic.Id();

        [Fact]
        public async Task HappyPath()
        {
            var request = AlexaRequests.CreateBoilerplateRequest();
            request.Content.RequestId = requestId;
            var virtualDirective = Util.QuickStub<IVirtualDirective>();
            var builder = new EngineBuilder<SkillRequest, SkillResponse>();
            builder
                .SetLogger(this)
                .SetResponseFactory(new AlexaResponseFactory())
                .AddInputModelBuilder(new FakeInputModelBuilder())
                .AddDirectiveProcessor(new FakeDirectiveProcessor(virtualDirective))
                .AddRequestHandler(new FakeRequestHandler(requestId, virtualDirective));
            var engine = builder.Build();
            var response = await engine.Evaluate(request);
            Assert.Equal(requestId, ((PlainTextOutputSpeech)response.Content.OutputSpeech).Text);
        }

        public ConversionEngineTest(ITestOutputHelper output) : base(output)
        {
        }

        private class FakeRequestHandler : IRequestHandler
        {
            private readonly string expectedRequestId;
            private readonly IVirtualDirective directiveToOutput;

            public FakeRequestHandler(
                string expectedRequestId,
                IVirtualDirective directiveToOutput)
            {
                this.expectedRequestId = expectedRequestId;
                this.directiveToOutput = directiveToOutput;
            }
            
            public bool CanHandle(ConversationContext context)
            {
                return context.RequestModel.RequestId == this.expectedRequestId;
            }

            public async Task Handle(ConversationContext context)
            {
                await Task.Factory.StartNew(() =>
                {
                    if (this.CanHandle(context))
                    {
                        context.OutputDirectives.Add(this.directiveToOutput);
                    }
                });
            }
        }

        private class FakeInputModelBuilder : IInputModelBuilder<SkillRequest>
        {
            public void Build(ConversationContext context, SkillRequest request)
            {
                context.RequestModel.RequestId = request.Content.RequestId;
            }
        }

        private class FakeDirectiveProcessor : IDirectiveProcessor<SkillRequest, SkillResponse>
        {
            private readonly IVirtualDirective targetDirective;

            public FakeDirectiveProcessor(IVirtualDirective targetDirective)
            {
                this.targetDirective = targetDirective;
            }
            
            public bool CanHandle(IVirtualDirective directive)
            {
                return targetDirective == directive;
            }

            public void Process(IVirtualDirective virtualDirective, SkillRequest request, SkillResponse response)
            {
                if (this.CanHandle(virtualDirective))
                {
                    response.Content.OutputSpeech = new PlainTextOutputSpeech
                    {
                        Text = request.Content.RequestId
                    };
                }
            }
        }
    }
}