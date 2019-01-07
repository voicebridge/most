using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VoiceBridge.Most
{
    /// <summary>
    /// A request handler with a fluent API for a quick and readable handler
    /// </summary>
    public class IntentConfiguration : IRequestHandler
    {
        private readonly List<string> intentNames = new List<string>();
        private readonly List<Func<ConversationContext, bool>> conditions = new List<Func<ConversationContext, bool>>();
        private readonly List<Func<ConversationContext, IVirtualDirective>> actionsToPerform = new List<Func<ConversationContext, IVirtualDirective>>();
        private readonly List<Func<ConversationContext, IEnumerable<IVirtualDirective>>> actionsToPerform2 = new List<Func<ConversationContext, IEnumerable<IVirtualDirective>>>();
        private readonly RequestType typeOfRequestToMatch = RequestType.Intent;

        /// <summary>
        /// Initialize with a single intent match
        /// </summary>
        /// <param name="intentName">Name of intent to match (case insensitive)</param>
        public IntentConfiguration(string intentName)
        {
            this.intentNames.Add(intentName);
        }

        /// <summary>
        /// Initializes with multiple intent name match. Conditions will be evaluated if any of the intents match current
        /// request. Intent names are case insensitive
        /// </summary>
        /// <param name="intentNames">Names of intent to match (OR list, case insensitive)</param>
        public IntentConfiguration(IEnumerable<string> intentNames)
        {
            this.intentNames.AddRange(intentNames);
        }
        
        /// <summary>
        /// Match on any non-intent request. Will throw an exception if you pass
        /// Intent as request type
        /// </summary>
        /// <param name="requestType">Request Type</param>
        public IntentConfiguration(RequestType requestType)
        {
            if (requestType == RequestType.Intent)
            {
                throw new ArgumentException("Must be a non-intent type");
            }

            this.typeOfRequestToMatch = requestType;
        }

        /// <summary>
        /// Match on any intent
        /// </summary>
        public IntentConfiguration()
        {
        }
        
        /// <summary>
        /// Adds a matching condition. if intent name matching has passed,
        /// conditions will be evaluated. This supports multiple conditions
        /// </summary>
        /// <param name="condition">Condition to evaluate</param>
        /// <returns>Itself</returns>
        public IntentConfiguration When(Func<ConversationContext, bool> condition)
        {
            this.conditions.Add(condition);
            return this;
        }
        
        /// <summary>
        /// True when a parameter is missing
        /// </summary>
        /// <param name="parameterName">parameter name</param>
        /// <returns>Itself</returns>
        public IntentConfiguration WhenParameterIsMissing(string parameterName)
        {
            return When(context => !context.RequestModel.ParameterHasValue(parameterName));
        }
        
        /// <summary>
        /// True when a parameter has a value
        /// </summary>
        /// <param name="parameterName">parameter name</param>
        /// <returns>Itself</returns>
        public IntentConfiguration WhenParameterIsSupplied(string parameterName)
        {
            return When(context => context.RequestModel.ParameterHasValue(parameterName));
        }
        
        /// <summary>
        /// Perform an action if all conditions match
        /// </summary>
        /// <param name="func">Function to execute (must return a virtual directive)</param>
        /// <returns>Itself</returns>
        public IntentConfiguration Do(Func<ConversationContext, IVirtualDirective> func)
        {
            this.actionsToPerform.Add(func);
            return this;
        } 

        /// <summary>
        /// Perform an action if all conditions match
        /// </summary>
        /// <param name="func">Function to execute (must return a an IEnumerable of virtual directives)</param>
        /// <returns>Itself</returns>
        public IntentConfiguration Do(Func<ConversationContext, IEnumerable<IVirtualDirective>> func)
        {
            this.actionsToPerform2.Add(func);
            return this;
        } 
        
        /// <summary>
        /// Request Handler CanHandle
        /// </summary>
        /// <param name="context">Conversation Context</param>
        /// <returns>True if this request handler can handle</returns>
        public bool CanHandle(ConversationContext context)
        {
            if (context.RequestType != typeOfRequestToMatch)
            {
                return false;
            }
            
            if (this.intentNames.Count != 0 && 
                this.intentNames.All(x => string.Compare(x, context.RequestModel.IntentName, StringComparison.InvariantCultureIgnoreCase) != 0))
            {
                return false;
            }

            if (this.conditions.Any())
            {
                return this.conditions.All(condition => condition(context));
            }

            return true;
        }

        /// <summary>
        /// Handle Request
        /// </summary>
        /// <param name="context">Conversation Context</param>
        /// <returns>Task</returns>
        public Task Handle(ConversationContext context)
        {
            return Task.Run(() =>
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
    }
}
