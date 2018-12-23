using System;
using System.Threading.Tasks;
using VoiceBridge.Most.Logging;
using VoiceBridge.Most.VoiceModel;

namespace VoiceBridge.Most
{
    public class ConversationEngine<TRequest, TResponse> : IConversationEngine<TRequest, TResponse>
        where TRequest : IRequest
        where TResponse : IResponse
    {
        private readonly IInputModelBuilder<TRequest> inputModelBuilder;
        private readonly IResponseFactory<TResponse> responseFactory;
        private readonly IRequestHandler compositeHandler;
        private readonly IDirectiveProcessor<TRequest, TResponse> compositeProcessor;
        private readonly ILogger logger;

        public ConversationEngine(
            IInputModelBuilder<TRequest> inputModelBuilder,
            IResponseFactory<TResponse> responseFactory,
            IRequestHandler compositeHandler,
            IDirectiveProcessor<TRequest, TResponse> compositeProcessor,
            ILogger logger,
            IMetricsReporter metricsReporter)
        {
            Util.AssertNotNull(inputModelBuilder, nameof(inputModelBuilder));
            Util.AssertNotNull(responseFactory, nameof(responseFactory));
            Util.AssertNotNull(compositeHandler, nameof(compositeHandler));
            Util.AssertNotNull(compositeProcessor, nameof(compositeProcessor));
            
            this.inputModelBuilder = inputModelBuilder;
            this.responseFactory = responseFactory;
            this.compositeHandler = compositeHandler;
            this.compositeProcessor = compositeProcessor;
            this.logger = logger;
        }
        
        public async Task<TResponse> Evaluate(TRequest request)
        {
            this.logger.Debug("BEGIN REQUEST PROCESSING");
            var context = CreateConversationContext(request);
            await ProcessRequest(context);
            var response = BuildResponse(context, request);
            this.logger.Debug("END REQUEST PROCESSING");
            return response;
        }
        
        private TResponse BuildResponse(ConversationContext context, TRequest request)
        {
            this.logger.Debug($"Building response. Number of virtual directives: {context.OutputDirectives.Count}");
            var response = this.responseFactory.Create(context);

            foreach (var virtualDirective in context.OutputDirectives)
            {
                this.logger.Debug($"Processing virtual directive: {virtualDirective.GetType().FullName}");
                this.compositeProcessor.Process(virtualDirective, request, response);
            }

            return response;
        }

        private async Task ProcessRequest(ConversationContext context)
        {
            this.logger.Debug("Processing request: Invoking request handlers");
            await this.compositeHandler.Handle(context);
            this.logger.Debug("Request handlers invocation step completed.");
        }

        private ConversationContext CreateConversationContext(TRequest request)
        {
            var context = new ConversationContext();
            this.logger.Debug("Building conversation model...");
            this.inputModelBuilder.Build(context, request);
            this.logger.Debug($"Conversation model: {context.ToJson()}");
            return context;
        }
    }
}