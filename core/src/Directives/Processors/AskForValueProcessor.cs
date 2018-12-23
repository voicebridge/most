using VoiceBridge.Most.Alexa;
using VoiceBridge.Most.VoiceModel.Alexa;
using VoiceBridge.Most.VoiceModel.Alexa.Directives;
using VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK;
using VoiceBridge.Most.VoiceModel.GoogleAssistant.DialogFlow;

namespace VoiceBridge.Most.Directives.Processors
{
    public class AskForValueProcessor : DirectiveProcessorBase<AskForValueDirective>
    {
        protected override void Process(AskForValueDirective directive,SkillRequest request, SkillResponse response)
        {
            var elicitDirective = new ElicitSlotDialogDirective(directive.ParameterName)
            {
                UpdatedIntent = request.Content.Intent
            };
            response.Content.OutputSpeech = directive.Prompt.ToAlexaSpeech();
            
            response.Content.Directives.Add(elicitDirective);
        }

        protected override void Process(AskForValueDirective directive, AppRequest request, AppResponse response)
        {
            response.Payload.Body.ExpectUserResponse = true;
            response.Payload.Body.RichResponse.Items.Add(new SimpleResponseItem
            {
                Value = new SimpleResponse
                {
                    DisplayText = directive.Prompt.Content,
                    TextToSpeech = directive.Prompt.Content
                }
            });
        }
    }
}