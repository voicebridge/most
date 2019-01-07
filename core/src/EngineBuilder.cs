using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using VoiceBridge.Most.Directives.Processors;
using VoiceBridge.Most.Logging;
using VoiceBridge.Most.VoiceModel;

namespace VoiceBridge.Most
{
    /// <summary>
    /// Engine Builder
    /// </summary>
    /// <typeparam name="TRequest">Request type</typeparam>
    /// <typeparam name="TResponse">Response type</typeparam>
    public class EngineBuilder<TRequest, TResponse> 
        where TRequest : IRequest 
        where TResponse : IResponse
    {        
        private readonly ServiceCollection components = new ServiceCollection();
        
        /// <summary>
        /// Adds an input model builder.
        /// </summary>
        /// <param name="inputModelBuilder">Input Model Builder to run on every request</param>
        /// <returns>Itself</returns>
        public EngineBuilder<TRequest, TResponse> AddInputModelBuilder(IInputModelBuilder<TRequest> inputModelBuilder)
        {
            this.components.AddTransient((p) => inputModelBuilder);
            return this;
        }

        /// <summary>
        /// Set the response factory. Only 1 response factory can be set
        /// </summary>
        /// <param name="responseFactory">Response Factory</param>
        /// <returns>Itself</returns>
        public EngineBuilder<TRequest, TResponse> SetResponseFactory(IResponseFactory<TResponse> responseFactory)
        {
            this.components.AddTransient(p => responseFactory);
            return this;
        }

        /// <summary>
        /// Adds a request handler to trigger on every request (if it can handle)
        /// </summary>
        /// <param name="handler">Handler to add</param>
        /// <returns>Itself</returns>
        public EngineBuilder<TRequest, TResponse> AddRequestHandler(IRequestHandler handler)
        {
            this.components.AddTransient(p => handler);
            return this;
        }

        /// <summary>
        /// Sets the primary logger
        /// </summary>
        /// <param name="logger">Logger</param>
        /// <returns>Itself</returns>
        public EngineBuilder<TRequest, TResponse> SetLogger(ILogger logger)
        {
            this.components.AddTransient(p => logger);
            return this;
        }

        /// <summary>
        /// Sets the metrics reporter
        /// </summary>
        /// <param name="reporter">reporter</param>
        /// <returns>Itself</returns>
        public EngineBuilder<TRequest, TResponse> SetMetricsReporter(IMetricsReporter reporter)
        {
            this.components.AddTransient(p => reporter);
            return this;
        }

        /// <summary>
        /// Adds a virtual directive processor
        /// </summary>
        /// <param name="directiveProcessor">Directive processor</param>
        /// <returns>Itself</returns>
        public EngineBuilder<TRequest, TResponse> AddDirectiveProcessor(IDirectiveProcessor<TRequest, TResponse> directiveProcessor)
        {
            this.components.AddTransient(p => directiveProcessor);
            return this;
        }

        /// <summary>
        /// Builds a conversation engine
        /// </summary>
        /// <returns>Conversation Engine</returns>
        public IConversationEngine<TRequest, TResponse> Build()
        {
            var provider = this.components.BuildServiceProvider();
            var logger = provider.GetService<ILogger>() ?? new NullLoggerReporter();
            var compositeInputBuilder = CreateInputModelBuilder(provider);
            var responseFactory = CreateResponseFactory(provider);
            var compositeHandler = CreateCompositeHandler(provider, logger);
            var directiveProcessor = CreateCompositeDirectiveProcessor(provider, logger);
            var reporter = provider.GetService<IMetricsReporter>() ?? new NullLoggerReporter();
            
            return new ConversationEngine<TRequest, TResponse>(
                compositeInputBuilder, 
                responseFactory,
                compositeHandler,
                directiveProcessor,
                logger,
                reporter);
        }

        private IDirectiveProcessor<TRequest, TResponse> CreateCompositeDirectiveProcessor(
            IServiceProvider provider,
            ILogger logger)
        {
            return new CompositeDirectiveProcessor<TRequest, TResponse>(provider.GetServices<IDirectiveProcessor<TRequest, TResponse>>(), logger);
        }

        private IRequestHandler CreateCompositeHandler(IServiceProvider provider, ILogger logger)
        {
            return new CompositeHandler(provider.GetServices<IRequestHandler>(), logger);
        }

        private IResponseFactory<TResponse> CreateResponseFactory(IServiceProvider provider)
        {
            return provider.GetService<IResponseFactory<TResponse>>();
        }

        private IInputModelBuilder<TRequest> CreateInputModelBuilder(IServiceProvider provider)
        {
            return new CompositeInputModelBuilder<TRequest>(provider.GetServices<IInputModelBuilder<TRequest>>());
        }
    }
}