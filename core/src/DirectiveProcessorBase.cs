using System.Diagnostics;
using VoiceBridge.Most.VoiceModel.Alexa;
using VoiceBridge.Most.VoiceModel.GoogleAssistant;
using VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK;
using VoiceBridge.Most.VoiceModel.GoogleAssistant.DialogFlow;

namespace VoiceBridge.Most
{
    public abstract class DirectiveProcessorBase<TDirective> : 
        IDirectiveProcessor<SkillRequest, SkillResponse> ,
        IDirectiveProcessor<AppRequest, AppResponse>
        where TDirective : IVirtualDirective
    {
        public bool CanHandle(IVirtualDirective directive)
        {
            if (directive == null)
            {
                return false;
            }

            return 
                directive.GetType() == typeof(TDirective) && 
                OnCanHandle((TDirective)directive);
        }

        public void Process(IVirtualDirective virtualDirective, SkillRequest request, SkillResponse response)
        {
            if (!this.CanHandle(virtualDirective))
            {
                return;
            }

            this.Process((TDirective) virtualDirective, request, response);
        }
        
        public void Process(IVirtualDirective virtualDirective, AppRequest request, AppResponse response)
        {
            if (!this.CanHandle(virtualDirective))
            {
                return;
            }

            this.Process((TDirective) virtualDirective, request, response);
        }

        protected virtual bool OnCanHandle(TDirective directive)
        {
            return true;
        }

        protected abstract void Process(
            TDirective directive, 
            SkillRequest request, 
            SkillResponse response);

        protected abstract void Process(
            TDirective directive, 
            AppRequest request,
            AppResponse response);
    }
}