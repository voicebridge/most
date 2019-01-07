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
        private readonly ILogger baseLogger;
        private readonly IMetricsReporter metricsReporter;
        private readonly ISessionStateStore sessionStore;

        public ConversationEngine(IServiceProvider serviceProvider)
        {
            Util.AssertNotNull(serviceProvider, nameof(serviceProvider));
            this.inputModelBuilder = serviceProvider.GetService<CompositeInputModelBuilder<TRequest>>();
            this.responseFactory = serviceProvider.GetService<IResponseFactory<TResponse>>();
            this.compositeHandler = serviceProvider.GetService<CompositeHandler>();
            this.compositeProcessor = serviceProvider.GetService<CompositeDirectiveProcessor<TRequest, TResponse>>();
            this.baseLogger = serviceProvider.GetService<ILogger>() ?? new NullLoggerReporter();
            this.metricsReporter = serviceProvider.GetService<IMetricsReporter>() ?? new NullLoggerReporter();
            this.sessionStore = serviceProvider.GetService<ISessionStateStore>();
            this.metricsReporter = new SafeMetricsReporter(this.metricsReporter, this.baseLogger);
        }
        
        public async Task<TResponse> Evaluate(TRequest request)
        {
            TResponse response;
            
            using (this.metricsReporter.MeasureTime(MetricNames.FullEvaluationTime))
            {
                var logger = new ScopedLogger(this.baseLogger);
                logger.Debug("BEGIN PROCESSING REQUEST");

                var context = CreateConversationContext(request, logger);
                logger.CurrentContext = context;

                using (this.metricsReporter.MeasureTime(MetricNames.SessionRestoreTime))
                {
                    await this.RestoreState(context, logger);
                }

                using (this.metricsReporter.MeasureTime(MetricNames.ExecutingRequestHandlers))
                {
                    await this.ProcessRequest(context, logger);
                }

                using (this.metricsReporter.MeasureTime(MetricNames.ProcessingVirtualDirectives))
                {
                    response = this.BuildResponse(context, request, logger);
                }

                using (this.metricsReporter.MeasureTime(MetricNames.SessionSaveTime))
                {
                    await this.SaveState(context, logger);
                }

                logger.Debug("END REQUEST PROCESSING");
            }

            return response;
        }

        private async Task SaveState(ConversationContext context, ILogger logger)
        {
            if (this.sessionStore == null)
            {
                return;
            }

            try
            {
                logger.Debug("Attempting to save session state");
                await this.sessionStore.SaveAsync(context);
                logger.Debug("Session state saved!");
            }
            catch (Exception exception)
            {
                logger.Error($"An error has occured while saving session state: ${exception}");
            }
        }

        private async Task RestoreState(ConversationContext context, ILogger logger)
        {
            if (this.sessionStore == null)
            {
                return;
            }

            try
            {
                using (this.metricsReporter.MeasureTime(MetricNames.SessionRestoreTime))
                {
                    logger.Debug("Attempting to restore session state");
                    await this.sessionStore.LoadAsync(context);
                    logger.Debug("Session state restored!");
                }
            }
            catch (Exception exception)
            {
                logger.Error($"An error has occured while restoring session state: ${exception}");
            }
        }

        private TResponse BuildResponse(ConversationContext context, TRequest request, ILogger logger)
        {
            logger.Debug($"Building response. Number of virtual directives: {context.OutputDirectives.Count}");
            var response = this.responseFactory.Create(context);

            foreach (var virtualDirective in context.OutputDirectives)
            {
                logger.Debug($"Processing virtual directive: {virtualDirective.GetType().FullName}");
                this.compositeProcessor.Process(virtualDirective, request, response);
            }

            return response;
        }

        private async Task ProcessRequest(ConversationContext context, ILogger logger)
        {
            logger.Debug("Processing request: Invoking request handlers");
            await this.compositeHandler.Handle(context);
            logger.Debug("Request handlers invocation step completed.");
        }

        private ConversationContext CreateConversationContext(TRequest request, ILogger logger)
        {
            var context = new ConversationContext();
            logger.Debug("Building conversation model...");
            this.inputModelBuilder.Build(context, request);
            logger.Debug($"Conversation model: {context.ToJson()}");
            return context;
        }
    }
}