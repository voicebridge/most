namespace VoiceBridge.Most
{
    /// <summary>
    /// Describes a factory that can create instances of a specific response type
    /// </summary>
    /// <typeparam name="TResponse"></typeparam>
    public interface IResponseFactory<out TResponse>
    {
        /// <summary>
        /// Create an instance of the given response
        /// </summary>
        /// <param name="context">Conversation Context for the current request</param>
        /// <returns>Boilerplate Response</returns>
        TResponse Create(ConversationContext context);
    }
}