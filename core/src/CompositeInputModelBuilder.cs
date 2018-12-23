using System.Collections.Generic;
using System.Linq;

namespace VoiceBridge.Most
{
    public class CompositeInputModelBuilder<TRequest> : IInputModelBuilder<TRequest>
    {
        private readonly IEnumerable<IInputModelBuilder<TRequest>> inputModelBuilders;

        public CompositeInputModelBuilder(IEnumerable<IInputModelBuilder<TRequest>> inputModelBuilders)
        {
            this.inputModelBuilders = inputModelBuilders;
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