using System;
using System.Collections.Generic;
using System.Linq;
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
            this.components.AddTransient<IRequestHandler>(p => handler);
            return this;
        }

        /// <summary>
        /// Sets the primary logger
        /// </summary>
        /// <param name="logger">Logger</param>
        /// <returns>Itself</returns>
        public EngineBuilder<TRequest, TResponse> SetLogger(ILogger logger)
        {
            this.components.AddTransient<ILogger>(p => logger);
            return this;
        }

        /// <summary>
        /// Sets the metrics reporter
        /// </summary>
        /// <param name="reporter">reporter</param>
        /// <returns>Itself</returns>
        public EngineBuilder<TRequest, TResponse> SetMetricsReporter(IMetricsReporter reporter)
        {
            this.components.AddTransient<IMetricsReporter>(p => reporter);
            return this;
        }

        /// <summary>
        /// Adds a virtual directive processor
        /// </summary>
        /// <param name="directiveProcessor">Directive processor</param>
        /// <returns>Itself</returns>
        public EngineBuilder<TRequest, TResponse> AddDirectiveProcessor(IDirectiveProcessor<TRequest, TResponse> directiveProcessor)
        {
            this.components.AddTransient<IDirectiveProcessor<TRequest, TResponse>>(p => directiveProcessor);
            return this;
        }

        /// <summary>
        /// Sets session state persistence
        /// </summary>
        /// <param name="store">Store instance</param>
        /// <returns>Itself</returns>
        public EngineBuilder<TRequest, TResponse> SetSessionStore(ISessionStateStore store)
        {
            this.components.AddTransient<ISessionStateStore>(p => store);
            return this;
        }

        /// <summary>
        /// Builds a conversation engine
        /// </summary>
        /// <returns>Conversation Engine</returns>
        public IConversationEngine<TRequest, TResponse> Build()
        {
            this.AddRequiredSupportComponents();
            var provider = this.components.BuildServiceProvider();
            return new ConversationEngine<TRequest, TResponse>(provider);
        }

        private void AddRequiredSupportComponents()
        {
            if (this.components.All(x => x.ServiceType != typeof(ILogger)))
            {
                this.components.AddTransient<ILogger, NullLoggerReporter>();
            }

            if (this.components.All(x => x.ServiceType != typeof(IMetricsReporter)))
            {
                this.components.AddTransient<IMetricsReporter, NullLoggerReporter>();
            }
        }
    }
}