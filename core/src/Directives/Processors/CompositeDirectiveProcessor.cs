using System.Collections.Generic;
using System.Linq;
using VoiceBridge.Most.Logging;
using VoiceBridge.Most.VoiceModel;

namespace VoiceBridge.Most.Directives.Processors
{
    internal class CompositeDirectiveProcessor<TRequest, TResponse> : IDirectiveProcessor<TRequest, TResponse>
        where TRequest : IRequest
        where TResponse : IResponse
    {
        private readonly IEnumerable<IDirectiveProcessor<TRequest, TResponse>> processors;
        private readonly ILogger logger;

        public CompositeDirectiveProcessor(IEnumerable<IDirectiveProcessor<TRequest, TResponse>> processors, ILogger logger)
        {
            this.processors = processors;
            this.logger = logger;
        }
        
        public bool CanHandle(IVirtualDirective directive)
        {
            if (directive == null)
            {
                this.logger.Debug("Directive is null");
                return false;
            }

            var name = directive.GetType().FullName;
            var processor = processors.FirstOrDefault(p => p.CanHandle(directive));
            if (processor == null)
            {
                this.logger.Debug($"Could not find any processor for directive ${directive}");
                return false;
            }

            var processorName = processor.GetType().FullName;
            this.logger.Debug($"Found processor {processorName} for directive {name}");
            return true;
        }

        public void Process(IVirtualDirective virtualDirective, TRequest request, TResponse response)
        {
            if (virtualDirective == null)
            {
                this.logger.Debug("Directive is null. Cannot process");
                return;
            }
            
            var name = virtualDirective.GetType().FullName;
            foreach (var processor in this.processors.Where(p => p.CanHandle(virtualDirective)))
            {
                this.logger.Debug($"Processing directive {name} using {processor.GetType().FullName}");
                processor.Process(virtualDirective, request, response);
            }
        }
    }
}