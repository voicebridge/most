using VoiceBridge.Most.VoiceModel;

namespace VoiceBridge.Most
{
    /// <summary>
    /// Describes Directive Processors. These are objects that can take a virtual directive and populate
    /// the response with platform specific objects
    /// </summary>
    /// <typeparam name="TRequest">Request Type (Must inherit from IRequest)</typeparam>
    /// <typeparam name="TResponse">Response Type (Must inherit from IResponse)</typeparam>
    public interface IDirectiveProcessor<in TRequest, in TResponse>
        where TRequest : IRequest
        where TResponse : IResponse
    {
        /// <summary>
        /// Does this processor support the given directive?
        /// </summary>
        /// <param name="directive">Directive to check</param>
        /// <returns>True if directive is supported</returns>
        bool CanHandle(IVirtualDirective directive);
        
        /// <summary>
        /// Convert the virtual directive into an object model that the platform can understand and populate the response
        /// with the result
        /// </summary>
        /// <param name="virtualDirective">Virtual Directive to process</param>
        /// <param name="request">Request</param>
        /// <param name="response">Response to populate</param>
        void Process(IVirtualDirective virtualDirective, TRequest request, TResponse response);
    }
}