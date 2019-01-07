using System.Threading.Tasks;

namespace VoiceBridge.Most
{
    public interface ISessionStateStore
    {
        Task SaveState(ConversationContext context);
        
        Task RestoreState(ConversationContext context);
    }
}