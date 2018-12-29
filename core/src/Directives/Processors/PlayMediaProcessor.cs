using VoiceBridge.Most.VoiceModel.Alexa;
using VoiceBridge.Most.VoiceModel.GoogleAssistant.DialogFlow;

namespace VoiceBridge.Most.Directives.Processors
{
    public class PlayMediaProcessor : DirectiveProcessorBase<PlayMediaDirective>
    {
        protected override void Process(PlayMediaDirective directive, SkillRequest request, SkillResponse response)
        {
            throw new System.NotImplementedException();
        }

        protected override void Process(PlayMediaDirective directive, AppRequest request, AppResponse response)
        {
            throw new System.NotImplementedException();
        }
    }
}