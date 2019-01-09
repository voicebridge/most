using System;
using System.Collections.Generic;

namespace VoiceBridge.Most
{
    public interface IRequestHandlerBuilder : IRequestHandler
    {
        /// <summary>
        /// Gets the target request type this builder will match on
        /// </summary>
        RequestType TypeOfRequestToMatch { get; }
        
        /// <summary>
        /// Adds a condition to evaluate
        /// </summary>
        /// <param name="condition">Condition to evaluate</param>
        /// <returns>Itself</returns>
        IRequestHandlerBuilder When(Func<ConversationContext, bool> condition);
        
        /// <summary>
        /// Perform an action if ALL conditions match
        /// </summary>
        /// <param name="func">Function to execute (must return a virtual directive)</param>
        /// <returns>Itself</returns>
        IRequestHandlerBuilder Do(Func<ConversationContext, IVirtualDirective> func);

        /// <summary>
        /// Perform an action if ALL conditions match
        /// </summary>
        /// <param name="func">Function to execute (must return a an IEnumerable of virtual directives)</param>
        /// <returns>Itself</returns>
        IRequestHandlerBuilder Do(Func<ConversationContext, IEnumerable<IVirtualDirective>> func);
    }
}