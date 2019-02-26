using VoiceBridge.Most.VoiceModel.Alexa;
using VoiceBridge.Most.VoiceModel.GoogleAssistant.DialogFlow;

namespace VoiceBridge.Most.Directives.Processors
{
    internal class NoOpDirectiveProcessor : DirectiveProcessorBase<NoOpDirective>
    {
        protected override void Process(NoOpDirective directive, SkillRequest request, SkillResponse response)
        {
            response.Content = null;
            response.SessionAttributes = null;
        }

        protected override void Process(NoOpDirective directive, AppRequest request, AppResponse response)
        {
            response.Source = null;
            response.Payload = null;
            response.Messages = null;
            response.FulfillmentText = null;
        }
    }
}