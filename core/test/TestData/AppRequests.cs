using System.Collections.Generic;
using VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK;
using VoiceBridge.Most.VoiceModel.GoogleAssistant.DialogFlow;

namespace VoiceBridge.Most.Test.TestData
{
    public class AppRequests
    {
        public static AppRequest CreateBoileRequest()
        {
            var request = new AppRequest
            {
                Result = new QueryResult
                {
                    Parameters = new Dictionary<string, string>(),
                    FulfillmentMessages = new List<FulfillmentMessage>(),
                    OutputContexts = new List<OutputContext>(),
                    Intent = new IntentDescription()
                },
                OriginalDetectIntentRequest = new Payload
                {
                    Content = new ActionRequest
                    {
                        AvailableSurfaces = new List<Surface>(),
                        Conversation = new Conversation(),
                        Device = new Device {Location = new Location {PostalAddress = new PostalAddress()}},
                        Inputs = new List<Input>(),
                        Surface = new Surface(),
                        User = new UserInfo()
                    }
                }
            };

            return request;
        }
    }
}