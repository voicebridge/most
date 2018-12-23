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
        [Fact]
        public async Task EvaluateHappyPathThroughBuilder()
        {
            var request = new SkillRequest();
            var context = new ConversationContext();
            var response = AlexaResponses.Boilerplate();
            var vDirective = Util.QuickMock<IVirtualDirective>();
            var handler = new FakeCompositeHandler(context.RequestModel, vDirective);
            var flagName = Generic.Id();
            
            var inputModelBuilder = CreateInputModelBuilder(request, context);
            var directiveProcessor = Util.CreateDirectiveProcessor(
                                        response, 
                                        vDirective,
                                        (d, r) =>
                                        {
                                            Assert.Same(response, r);
                                            Assert.Same(vDirective, d);
                                            response.SessionAttributes[flagName] = "true";
                                        });
            
            var builder = new EngineBuilder<SkillRequest, SkillResponse>();
            builder.AddInputModelBuilder(inputModelBuilder);
            builder.SetResponseFactory(CreateResponseFactory(context.RequestModel, response));
            builder.AddRequestHandler(handler); 
            builder.AddDirectiveProcessor(directiveProcessor.Object);
            builder.SetLogger(this);
            
            var engine = builder.Build();
            var actualResponse = await engine.Evaluate(request);
            Assert.Same(response, actualResponse);
            Assert.Equal("true", response.SessionAttributes[flagName]);   
        }

        private IResponseFactory<SkillResponse> CreateResponseFactory(RequestModel requestModel, SkillResponse response)
        {
            var mock = new Mock<IResponseFactory<SkillResponse>>();
            mock.Setup(x => x.Create(It.Is<ConversationContext>(ctx => ctx.RequestModel == requestModel))).Returns(response);
            return mock.Object;
        }

        private IInputModelBuilder<SkillRequest> CreateInputModelBuilder(
            SkillRequest request, 
            ConversationContext context)
        {
            var mock = new Mock<IInputModelBuilder<SkillRequest>>();
            mock.Setup(x => x.Build(context, request));
            return mock.Object;
        }

        private class FakeCompositeHandler : IRequestHandler
        {
            private readonly RequestModel expectedInputRequestModel;
            private readonly IVirtualDirective directive;

            public FakeCompositeHandler(
                RequestModel expectedInputRequestModel, 
                IVirtualDirective directive)
            {
                this.expectedInputRequestModel = expectedInputRequestModel;
                this.directive = directive;
            }

            public bool CanHandle(ConversationContext context)
            {
                return context.RequestModel == expectedInputRequestModel;
            }

            public async Task Handle(ConversationContext context)
            {
                Assert.Empty(context.OutputDirectives);
                await Task.Run(() => context.OutputDirectives.Add(directive));
            }
        }

        public ConversionEngineTest(ITestOutputHelper output) : base(output)
        {
        }
    }
}