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
        public void VerifyMediaStatusArgument()
        {
            var request = GetRequest(Files.GoogleMediaStatusFailed);
            var argument = request.Inputs[0].Arguments[0];
            Assert.Equal("FAILED", argument.Extension.Status);
        }
        
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
            Assert.Equal("ssddsds-5xSl_c", request.User.UserId);
            Assert.Equal("2018-12-20T07:25:17Z", request.User.LastSeen);
            Assert.Equal("en-US", request.User.Locale);
            Assert.Equal("fake-data", request.User.UserStorage);
        }

        [Fact]
        public void VerifyConversation()
        {
            var request = GetRequest();
            Assert.Equal("[]", request.Conversation.Token);
            Assert.Equal("ffdd-Q3yl6VbnPaPEaLBowp9LU9eWEPInzETqA3AhRGa3_epq40X_Qz6b", request.Conversation.Id);
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
            AssertSurfaceCapable(GoogleAssistantConstants.Capabilities.AudioMediaResponse);
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
            throw new Exception("Failed to find capability " + capabilityName);
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
            throw new Exception("Failed to find capability: " + capabilityName);
        }
        
        private static ActionRequest GetRequest(string json = null)
        {
            json = json ?? Files.SampleDialogFlowRequest;
            var app = JsonConvert.DeserializeObject<AppRequest>(json);
            return app.OriginalDetectIntentRequest.Content;
        }
    }
}