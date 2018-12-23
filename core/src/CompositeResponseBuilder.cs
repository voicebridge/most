using System.Collections.Generic;
using System.Linq;

namespace VoiceBridge.Most
{
    public class CompositeResponseBuilder<TResponse> : IResponseBuilder<TResponse> where TResponse : class
    {
        private readonly IEnumerable<IResponseBuilder<TResponse>> builders;

        public CompositeResponseBuilder(IEnumerable<IResponseBuilder<TResponse>> builders)
        {
            this.builders = builders;
        }
        
        public bool CanHandle(ConversationContext context)
        {
            return this.builders.Any(x => x.CanHandle(context));
        }

        public TResponse Build(ConversationContext context)
        {
            var builder = this.builders.FirstOrDefault(x => x.CanHandle(context));
            return builder?.Build(context);
        }
    }
}