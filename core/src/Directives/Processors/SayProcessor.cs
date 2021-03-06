using VoiceBridge.Most.Alexa;
using VoiceBridge.Most.Google;
using VoiceBridge.Most.VoiceModel.Alexa;
using VoiceBridge.Most.VoiceModel.GoogleAssistant;
using VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK;
using VoiceBridge.Most.VoiceModel.GoogleAssistant.DialogFlow;

namespace VoiceBridge.Most.Directives.Processors
{
    internal class SayProcessor : DirectiveProcessorBase<SayDirective>
    {
        protected override void Process(SayDirective directive, SkillRequest request, SkillResponse response)
        {
            response.Content.OutputSpeech = directive.Prompt.ToAlexaSpeech();
        }

        protected override void Process(SayDirective directive, AppRequest request, AppResponse response)
        {
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