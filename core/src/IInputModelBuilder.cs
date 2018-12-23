namespace VoiceBridge.Most
{
    public interface IInputModelBuilder<TRequest>
    {
        void Build(ConversationContext context, TRequest request);
    }
}