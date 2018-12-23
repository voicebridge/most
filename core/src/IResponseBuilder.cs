namespace VoiceBridge.Most
{
    public interface IResponseBuilder<TResponse>
    {
        bool CanHandle(ConversationContext context);
        TResponse Build(ConversationContext context);
    }
}