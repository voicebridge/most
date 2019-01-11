using System.Collections.Generic;
using System.Linq;
using VoiceBridge.Most.Logging;

namespace VoiceBridge.Most
{
    public class CompositeInputModelBuilder<TRequest> : IInputModelBuilder<TRequest>
    {
        private readonly IEnumerable<IInputModelBuilder<TRequest>> inputModelBuilders;
        private readonly ILogger logger;

        public CompositeInputModelBuilder(IEnumerable<IInputModelBuilder<TRequest>> inputModelBuilders,
            ILogger logger)
        {
            this.inputModelBuilders = inputModelBuilders;
            this.logger = logger;
        }

        public void Build(ConversationContext context, TRequest request)
        {
            foreach (var modelBuilder in this.inputModelBuilders)
            {
                modelBuilder.Build(context, request);
            }
        }
    }
}