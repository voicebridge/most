using System.Threading.Tasks;

namespace VoiceBridge.Most
{
    public interface IConversationEngine<TRequest, TResponse>
    {
        Task<TResponse> Evaluate(TRequest request);
    }
}