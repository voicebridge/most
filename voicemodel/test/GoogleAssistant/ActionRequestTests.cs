using System;
using System.Linq;
using Newtonsoft.Json;
using VoiceBridge.Most.VoiceModel.GoogleAssistant;
using VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK;
using VoiceBridge.Most.VoiceModel.GoogleAssistant.DialogFlow;
using Xunit;

namespace VoiceBridge.Most.VoiceModel.Test.GoogleAssistant
{
    public class ActionRequestTests
    {
        [Fact]
        public void GeneralProperties()
        {
            var request = GetRequest();
            Assert.True(request.IsInSandbox);
        }
        
        [Fact]
        public void VerifyUser()
        {
            var request = GetRequest();
            Assert.Equal("token-1", request.User.IdToken);
            Assert.Equal("2018-03-21T21:02:13Z", request.User.LastSeen);
            Assert.Equal("en-US", request.User.Locale);
            Assert.Equal("{\"data\":{}}", request.User.UserStorage);
        }

        [Fact]
        public void VerifyConversation()
        {
            var request = GetRequest();
            Assert.Equal("token-2", request.Conversation.Token);
            Assert.Equal("1524602877583", request.Conversation.Id);
            Assert.Equal(GoogleAssistantConstants.ConversationType.Active, request.Conversation.Type);
        }

        [Fact]
        public void VerifyInputs()
        {
            var input = GetRequest().Inputs.Single();
            Assert.Equal(GoogleAssistantConstants.CommonIntents.Text, input.Intent);
            Assert.IsType<QueryInput>(input.RawInputs.Single());
            Assert.IsType<TextOnlyArgument>(input.Arguments.Single());
        }

        [Fact]
        public void SurfaceCapabilities()
        {
            AssertSurfaceCapable(GoogleAssistantConstants.Capabilities.AudioOut);
            AssertSurfaceCapable(GoogleAssistantConstants.Capabilities.WebBrowser);
            AssertSurfaceCapable(GoogleAssistantConstants.Capabilities.AudioMediaResponse);
            AssertSurfaceCapable(GoogleAssistantConstants.Capabilities.ScreenOut);
        }
        
        [Fact]
        public void SurfaceAvailabilities()
        {
            AssertSurfaceAvailable(GoogleAssistantConstants.Capabilities.AudioOut);
            AssertSurfaceAvailable(GoogleAssistantConstants.Capabilities.ScreenOut);
        }

        private static void AssertSurfaceCapable(string capabilityName)
        {
            var request = GetRequest();
            foreach (var capability in request.Surface.Capabilities)
            {
                if (capability.Name == capabilityName)
                {
                    return;
                }
            }
            throw new Exception("Failed to find capability");
        }

        private static void AssertSurfaceAvailable(string capabilityName)
        {
            var request = GetRequest();
            foreach (var capability in request.AvailableSurfaces.Single().Capabilities)
            {
                if (capability.Name == capabilityName)
                {
                    return;
                }
            }
            throw new Exception("Failed to find capability");
        }
        
        private static ActionRequest GetRequest()
        {
            var app = JsonConvert.DeserializeObject<AppRequest>(Files.SampleDialogFlowRequest);
            return app.OriginalDetectIntentRequest.Content;
        }
    }
}