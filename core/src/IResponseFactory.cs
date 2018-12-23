namespace VoiceBridge.Most
{
    public interface IResponseFactory<TResponse>
    {
        TResponse Create(ConversationContext context);
    }
}