using System.Threading.Tasks;

namespace VoiceBridge.Most
{
    /// <summary>
    /// Request Handler
    /// </summary>
    public interface IRequestHandler
    {
        /// <summary>
        /// Given the provided context, can this handler handle this request?
        /// </summary>
        /// <param name="context">Conversation Context</param>
        /// <returns>True if handler can handle request</returns>
        bool CanHandle(ConversationContext context);
        
        /// <summary>
        /// Handle request (All handlers that can handle the request will be invoked)
        /// </summary>
        /// <param name="context">Conversation Context</param>
        /// <returns>Task (async)</returns>
        Task Handle(ConversationContext context);
    }
}