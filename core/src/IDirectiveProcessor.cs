using VoiceBridge.Most.VoiceModel;

namespace VoiceBridge.Most
{
    public interface IDirectiveProcessor<TRequest, TResponse>
        where TRequest : IRequest
        where TResponse : IResponse
    {
        bool CanHandle(IVirtualDirective directive);
        void Process(IVirtualDirective virtualDirective, TRequest request, TResponse response);
    }
}