using System.Threading.Tasks;

namespace VoiceBridge.Most
{
    /// <summary>
    /// Describes an object that can persist request data to an in-memory or durable store
    /// </summary>
    public interface ISessionStateStore
    {
        /// <summary>
        /// Save request state
        /// </summary>
        /// <param name="context">Conversation Context model for the current request</param>
        /// <returns>Task</returns>
        Task SaveAsync(ConversationContext context);
        
        /// <summary>
        /// Load state into the given the conversation context
        /// </summary>
        /// <param name="context">Conversation Context to populate</param>
        /// <returns>Task</returns>
        Task LoadAsync(ConversationContext context);
    }
}