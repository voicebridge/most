using System.Linq;
using VoiceBridge.Most.Alexa;
using VoiceBridge.Most.Google;
using VoiceBridge.Most.VoiceModel.Alexa;
using VoiceBridge.Most.VoiceModel.Alexa.Directives;
using VoiceBridge.Most.VoiceModel.GoogleAssistant;
using VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK;
using VoiceBridge.Most.VoiceModel.GoogleAssistant.DialogFlow;

namespace VoiceBridge.Most.Directives.Processors
{
    public class ImageProcessor : DirectiveProcessorBase<ImageDirective>
    {
        protected override void Process(ImageDirective directive, SkillRequest request, SkillResponse response)
        {
            response.Content.ShouldEndSession = !directive.KeepSessionOpen;

            /*
            response.Content.Directives.Add(new ShowImageDirective
            {
            });
            */
        }

        protected override void Process(ImageDirective directive, AppRequest request, AppResponse response)
        {
            response.Payload.Body.ExpectUserResponse = directive.KeepSessionOpen;

            /*
            // If speech hasn't already been queued in the response, use the accessibility text
            if (!response.Payload.Body.RichResponse.Items.Any(x => x.GetType().Equals(typeof(SimpleResponse))))
            {
                response.Payload.Body.RichResponse.Items.Add(new SimpleResponseItem
                {
                    Value = new SimpleResponse
                    {
                        TextToSpeech = directive.AccessibilityText
                    }
                });
            }

            // Show the image using a basic card
            response.Payload.Body.RichResponse.Items.Add(new BasicCardItem
            {
                Value = new BasicCard
                {
                    Title = directive.AccessibilityText,
                    Image = new Image
                    {
                        AccessibilityText = directive.AccessibilityText,
                        Url = directive.Image,
                    }
                }
            });
            */
        }
    }
}