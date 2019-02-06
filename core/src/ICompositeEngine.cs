using System.Threading.Tasks;
using VoiceBridge.Most.VoiceModel;

namespace VoiceBridge.Most
{
    /// <summary>
    /// Describes an engine interface that operates at the IRequest/IResponse level
    /// </summary>
    public interface ICompositeEngine
    {
        /// <summary>
        /// Evaluate a request
        /// </summary>
        /// <param name="request">Request to evaluate</param>
        /// <returns>Resulting response</returns>
        Task<IResponse> Evaluate(IRequest request);
    }
}