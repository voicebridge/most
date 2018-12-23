using System.Collections.Generic;
using VoiceBridge.Most.VoiceModel.GoogleAssistant;
using VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK;
using VoiceBridge.Most.VoiceModel.GoogleAssistant.DialogFlow;

namespace VoiceBridge.Most.Google
{
    public class ActionResponseBuilder : IResponseFactory<AppResponse>
    {
        public AppResponse Create(ConversationContext context)
        {
            var response = new AppResponse()
            {
                Messages = new List<FulfillmentMessage>(),
                Payload = new ResponsePayload
                {
                    Body = new ResponseBody
                    {
                        RichResponse = new RichResponse
                        {
                            Items = new List<RichResponseItem>()
                        }
                    }
                }
            };
            return response;
        }
    }
}