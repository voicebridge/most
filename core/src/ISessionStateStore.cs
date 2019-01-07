using System.Threading.Tasks;

namespace VoiceBridge.Most
{
    public interface ISessionStateStore
    {
        Task SaveAsync(ConversationContext context);
        
        Task LoadAsync(ConversationContext context);
    }
}