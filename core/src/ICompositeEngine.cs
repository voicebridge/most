using System.Threading.Tasks;
using VoiceBridge.Most.VoiceModel;

namespace VoiceBridge.Most
{
    public interface ICompositeEngine
    {
        Task<IResponse> Evaluate(IRequest request);
    }
}