using System.Collections.Generic;
using Newtonsoft.Json;
using VoiceBridge.Most.VoiceModel.GoogleAssistant;
using VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK;
using VoiceBridge.Most.VoiceModel.GoogleAssistant.DialogFlow;

namespace VoiceBridge.Most.Google
{
    public class ActionResponseFactory : IResponseFactory<AppResponse>
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
            TransferSession(context, response);
            return response;
        }

        private void TransferSession(ConversationContext context, AppResponse response)
        {
            if (context.SessionStore.Count > 0)
            {
                response.Payload.Body.UserStorage = JsonConvert.SerializeObject(context.SessionStore);
            }
        }
    }
}