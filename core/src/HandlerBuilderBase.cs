using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VoiceBridge.Most
{
    public class RequestHandlerBuilder : IRequestHandlerBuilder
    {
        private readonly List<Func<ConversationContext, bool>> conditions = new List<Func<ConversationContext, bool>>();
        private readonly List<Func<ConversationContext, IVirtualDirective>> actionsToPerform = new List<Func<ConversationContext, IVirtualDirective>>();
        private readonly List<Func<ConversationContext, IEnumerable<IVirtualDirective>>> actionsToPerform2 = new List<Func<ConversationContext, IEnumerable<IVirtualDirective>>>();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="targetRequestType">Target Request Types</param>
        public RequestHandlerBuilder(RequestType targetRequestType)
        {
            this.TypeOfRequestToMatch = targetRequestType;
        }

        public RequestType TypeOfRequestToMatch { get; }

        /// <summary>
        /// Request Handler CanHandle
        /// </summary>
        /// <param name="context">Conversation Context</param>
        /// <returns>True if this request handler can handle</returns>
        public bool CanHandle(ConversationContext context)
        {
            if (context.RequestType != TypeOfRequestToMatch)
            {
                return false;
            }

            if (!this.conditions.All(x => x(context)))
            {
                return false;
            }

            return this.CanHandleSecondaryCheck(context);
        }
        
        /// <summary>
        /// Adds a condition to evaluate
        /// </summary>
        /// <param name="condition">Condition to evaluate</param>
        /// <returns>Itself</returns>
        public IRequestHandlerBuilder When(Func<ConversationContext, bool> condition)
        {
            this.conditions.Add(condition);
            return this;
        }
        
        /// <summary>
        /// Perform an action if ALL conditions match
        /// </summary>
        /// <param name="func">Function to execute (must return a virtual directive)</param>
        /// <returns>Itself</returns>
        public IRequestHandlerBuilder Do(Func<ConversationContext, IVirtualDirective> func)
        {
            this.actionsToPerform.Add(func);
            return this;
        } 

        /// <summary>
        /// Perform an action if ALL conditions match
        /// </summary>
        /// <param name="func">Function to execute (must return a an IEnumerable of virtual directives)</param>
        /// <returns>Itself</returns>
        public IRequestHandlerBuilder Do(Func<ConversationContext, IEnumerable<IVirtualDirective>> func)
        {
            this.actionsToPerform2.Add(func);
            return this;
        } 
        
        /// <summary>
        /// Handle Request
        /// </summary>
        /// <param name="context">Conversation Context</param>
        /// <returns>Task</returns>
        public async Task Handle(ConversationContext context)
        {
            await Task.Factory.StartNew(() =>
            {
                if (!this.CanHandle(context))
                {
                    return;
                }

                foreach (var action in this.actionsToPerform)
                {
                    context.OutputDirectives.Add(action(context));
                }

                foreach (var action in this.actionsToPerform2)
                {
                    context.OutputDirectives.AddRange(action(context));
                }
            });
        }

        protected virtual bool CanHandleSecondaryCheck(ConversationContext context)
        {
            return true;
        }
    }
}