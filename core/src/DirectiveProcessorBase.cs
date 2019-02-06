using System.Diagnostics;
using VoiceBridge.Most.VoiceModel.Alexa;
using VoiceBridge.Most.VoiceModel.GoogleAssistant;
using VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK;
using VoiceBridge.Most.VoiceModel.GoogleAssistant.DialogFlow;

namespace VoiceBridge.Most
{
    /// <summary>
    /// IDirectiveProcessor that supports both Alexa and Google Assistant
    /// </summary>
    /// <typeparam name="TDirective">Directive Type</typeparam>
    public abstract class DirectiveProcessorBase<TDirective> : 
        IDirectiveProcessor<SkillRequest, SkillResponse> ,
        IDirectiveProcessor<AppRequest, AppResponse>
        where TDirective : IVirtualDirective
    {
        /// <summary>
        /// Can this processor handle the given directive?
        /// </summary>
        /// <param name="directive">Directive to check</param>
        /// <returns>True if the directive is of a type that this processor supports</returns>
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

        /// <summary>
        /// Process virtual directive for an Alexa Request
        /// </summary>
        /// <param name="virtualDirective">Virtual directive to process</param>
        /// <param name="request">Current request</param>
        /// <param name="response">Response to populate</param>
        public void Process(IVirtualDirective virtualDirective, SkillRequest request, SkillResponse response)
        {
            if (!this.CanHandle(virtualDirective))
            {
                return;
            }

            this.Process((TDirective) virtualDirective, request, response);
        }
        
        
        /// <summary>
        /// Process virtual directive for a Google Request
        /// </summary>
        /// <param name="virtualDirective">Virtual directive to process</param>
        /// <param name="request">Current request</param>
        /// <param name="response">Response to populate</param>
        public void Process(IVirtualDirective virtualDirective, AppRequest request, AppResponse response)
        {
            if (!this.CanHandle(virtualDirective))
            {
                return;
            }

            this.Process((TDirective) virtualDirective, request, response);
        }

        /// <summary>
        /// When overriden, it enables the inheriting type to run additional checks prior to
        /// returning the result of CanHandle
        /// </summary>
        /// <param name="directive">Directive to check</param>
        /// <returns>True if this directive processor supports </returns>
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