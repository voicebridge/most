using System.Threading.Tasks;
using VoiceBridge.Most.VoiceModel;

namespace VoiceBridge.Most
{
    /// <summary>
    /// Describes an interface for an engine that can handle requests and return responses
    /// </summary>
    /// <typeparam name="TRequest">Request Type (Must inherit from IRequest)</typeparam>
    /// <typeparam name="TResponse">Response Type (Must Inherit from IResponse)</typeparam>
    public interface IConversationEngine<in TRequest, TResponse>
        where TRequest : IRequest
        where TResponse : IResponse
    {
        /// <summary>
        /// Evaluate a request and return a response
        /// </summary>
        /// <param name="request">Request to evaluate</param>
        /// <returns>Resulting response</returns>
        Task<TResponse> Evaluate(TRequest request);
    }
}