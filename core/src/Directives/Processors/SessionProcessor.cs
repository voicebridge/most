using VoiceBridge.Most.Alexa;
using VoiceBridge.Most.Google;
using VoiceBridge.Most.VoiceModel.Alexa;
using VoiceBridge.Most.VoiceModel.GoogleAssistant;
using VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK;
using VoiceBridge.Most.VoiceModel.GoogleAssistant.DialogFlow;

namespace VoiceBridge.Most.Directives.Processors
{
    internal class SessionProcessor : DirectiveProcessorBase<SessionDirective>
    {
        protected override void Process(SessionDirective directive, SkillRequest request, SkillResponse response)
        {
            response.Content.ShouldEndSession = false;
        }

        protected override void Process(SessionDirective directive, AppRequest request, AppResponse response)
        {
            response.Payload.Body.ExpectUserResponse = true;
        }
    }
}