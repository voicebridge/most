using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using VoiceBridge.Most.Logging;

namespace VoiceBridge.Most
{
    internal class CompositeHandler : IRequestHandler
    {
        private readonly IEnumerable<IRequestHandler> handlers;
        private readonly ILogger logger;

        public CompositeHandler(IEnumerable<IRequestHandler> handlers, ILogger logger)
        {
            this.handlers = handlers;
            this.logger = logger;
        }
        
        public bool CanHandle(ConversationContext context)
        {
            if(this.handlers.Any(x => x.CanHandle(context)))
            {
                this.logger.Debug("Handler for this request is available.");
                return true;
            }
            
            this.logger.Debug("Did not find any suitable handler");
            return false;
        }

        public async Task Handle(ConversationContext context)
        {
            foreach (var handler in this.handlers.Where(x => x.CanHandle(context)))
            {
                this.logger.Debug($"Executing handler: ${handler.GetType().FullName}");
                await handler.Handle(context);
            }
        }
    }
}