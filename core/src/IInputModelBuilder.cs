using VoiceBridge.Most.VoiceModel;

namespace VoiceBridge.Most
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    public interface IInputModelBuilder<in TRequest>
        where TRequest : IRequest
    {
        /// <summary>
        /// Populate the context as appropriate given the provided request
        /// </summary>
        /// <param name="context">Conversation Context Model</param>
        /// <param name="request">Current Request</param>
        void Build(ConversationContext context, TRequest request);
    }
}