using System;
using System.Threading.Tasks;
using VoiceBridge.Most.Logging;
using VoiceBridge.Most.VoiceModel;
using Microsoft.Extensions.DependencyInjection;
using VoiceBridge.Most.Directives.Processors;

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
        private readonly IMetricsReporter metricsReporter;
        private readonly ISessionStateStore sessionStore;

        public ConversationEngine(IServiceProvider serviceProvider)
        {
            Util.AssertNotNull(serviceProvider, nameof(serviceProvider));
            this.inputModelBuilder = serviceProvider.GetService<CompositeInputModelBuilder<TRequest>>();
            this.responseFactory = serviceProvider.GetService<IResponseFactory<TResponse>>();
            this.compositeHandler = serviceProvider.GetService<CompositeHandler>();
            this.compositeProcessor = serviceProvider.GetService<CompositeDirectiveProcessor<TRequest, TResponse>>();
            this.logger = serviceProvider.GetService<ILogger>() ?? new NullLoggerReporter();
            this.metricsReporter = serviceProvider.GetService<IMetricsReporter>() ?? new NullLoggerReporter();
            this.sessionStore = serviceProvider.GetService<ISessionStateStore>();
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