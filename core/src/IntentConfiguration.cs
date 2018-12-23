using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoiceBridge.Most.Directives;

namespace VoiceBridge.Most
{
    /// <summary>
    /// A request handler with a fluent API for a quick and readable handler
    /// </summary>
    public class IntentConfiguration : IRequestHandler
    {
        private readonly List<string> intentNames = new List<string>();
        private readonly List<Func<ConversationContext, bool>> conditions = new List<Func<ConversationContext, bool>>();
        private Func<IVirtualDirective> actionToPerform;

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
        /// Ask user for input
        /// </summary>
        /// <param name="parameterName">Parameter name (or slot name) to populate</param>
        /// <param name="promptText">Prompt plain text</param>
        /// <param name="expectedIntentName">Expected intent (optional)</param>
        /// <returns>Itself</returns>
        public IntentConfiguration AskFor(string parameterName, string promptText, string expectedIntentName = null)
        {
            var prompt = promptText.AsPrompt();
            return this.AskFor(parameterName, prompt, expectedIntentName);
        }

        /// <summary>
        /// Ask for user for input
        /// </summary>
        /// <param name="parameterName">Parameter name (or slot name) to populate</param>
        /// <param name="prompt">Prompt</param>
        /// <param name="expectedIntentName">Expected intent (optional)</param>
        /// <returns>Itself</returns>
        public IntentConfiguration AskFor(string parameterName, Prompt prompt, string expectedIntentName = null)
        {
            this.Do(() => new AskForValueDirective
            {
                ParameterName = parameterName,
                Prompt = prompt,
                ExpectedIntentName = expectedIntentName
            });
            return this;
        }

        /// <summary>
        /// Tell the user. (Terminal)
        /// </summary>
        /// <param name="promptText">Plain text to tell the user</param>
        /// <returns>Itself</returns>
        public IntentConfiguration Say(string promptText)
        {
            return this.Say(promptText.AsPrompt());
        }

        /// <summary>
        /// Tell the use (Terminal)
        /// </summary>
        /// <param name="prompt">Prompt to send back</param>
        /// <returns>Itself</returns>
        public IntentConfiguration Say(Prompt prompt)
        {
            this.Do(() => new SayDirective
            {
                Prompt = prompt
            });
            return this;
        }

        /// <summary>
        /// Perform an action if all conditions match
        /// </summary>
        /// <param name="func">Function to execute (must return a virtual directive)</param>
        /// <returns>Itself</returns>
        public IntentConfiguration Do(Func<IVirtualDirective> func)
        {
            this.actionToPerform = func;
            return this;
        } 

        /// <summary>
        /// Request Handler CanHandle
        /// </summary>
        /// <param name="context">Conversation Context</param>
        /// <returns>True if this request handler can handle</returns>
        public bool CanHandle(ConversationContext context)
        {
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
                if (this.actionToPerform != null)
                {
                    context.OutputDirectives.Add(this.actionToPerform());
                }
            });
        }
    }
}