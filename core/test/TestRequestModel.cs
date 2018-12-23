namespace VoiceBridge.Most.Test
{
    public class TestRequestModel : RequestModel
    {
        public ConversationContext AsConversationContext()
        {
            return new ConversationContext
            {
                RequestModel = this
            };
        }
    }
}