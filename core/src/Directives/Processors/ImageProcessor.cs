using System.Linq;
using VoiceBridge.Most.Alexa;
using VoiceBridge.Most.Google;
using VoiceBridge.Most.VoiceModel.Alexa;
using VoiceBridge.Most.VoiceModel.GoogleAssistant;
using VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK;
using VoiceBridge.Most.VoiceModel.GoogleAssistant.DialogFlow;

namespace VoiceBridge.Most.Directives.Processors
{
    public class ImageProcessor : DirectiveProcessorBase<ImageDirective>
    {
        protected override void Process(ImageDirective directive, SkillRequest request, SkillResponse response)
        {
            throw new System.NotImplementedException();
        }

        protected override void Process(ImageDirective directive, AppRequest request, AppResponse response)
        {
            response.Payload.Body.ExpectUserResponse = directive.KeepSessionOpen;

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

            // TODO: This is a pretty poor experience on a google home hub
            //       Consider using a URI on this device instead (until Google catch up with APL)

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
        }
    }
}