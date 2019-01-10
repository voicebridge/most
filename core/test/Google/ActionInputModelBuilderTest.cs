using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using VoiceBridge.Most.Google;
using VoiceBridge.Most.Test.TestData;
using VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK;
using VoiceBridge.Most.VoiceModel.GoogleAssistant.DialogFlow;
using Xunit;

namespace VoiceBridge.Most.Test.Google
{
    public class ActionInputModelBuilderTest
    {

        [Fact]
        public void NoInputRequest()
        {
            var appRequest = Files.GoogleNoInputRequest.FromJson<AppRequest>();
            var context = new ConversationContext();
            new ActionInputModelBuilder().Build(context, appRequest);
            Assert.Equal(RequestType.Other, context.RequestType);
        }

        [Fact]
        public void ImplicitIntentRequest()
        {
            var appRequest = JsonConvert.DeserializeObject<AppRequest>(Files.GoogleImplicitRequestSample);
            var context = new ConversationContext();
            new ActionInputModelBuilder().Build(context, appRequest);
            Assert.Equal(RequestType.Intent, context.RequestType);
        }
        
        [Fact]
        public void WelcomeIntent()
        {
            var appRequest = CreateRequestWithIntent("actions.intent.MAIN");
            appRequest.Result.Action = "input.welcome";
            appRequest.Result.Text = "GOOGLE_ASSISTANT_WELCOME";
            var context = new ConversationContext();
            new ActionInputModelBuilder().Build(context, appRequest);
            Assert.Equal(RequestType.Launch, context.RequestType);
        }
        
        [Fact]
        public void OptionSelected()
        {
            var appRequest = CreateRequestWithIntent("random");
            appRequest.Result.Text = "actions_intent_OPTION";
            var context = new ConversationContext();
            new ActionInputModelBuilder().Build(context, appRequest);
            Assert.Equal(RequestType.NonVoiceInputEvent, context.RequestType);
        }

        [Fact]
        public void UserInitiatedTermination()
        {
            var appRequest = CreateRequestWithIntent("actions.intent.CANCEL");
            appRequest.Result.Text = "actions_intent_CANCEL";
            var context = new ConversationContext();
            new ActionInputModelBuilder().Build(context, appRequest);
            Assert.Equal(RequestType.UserInitiatedTermination, context.RequestType);
        }
        
        [Fact]
        public void Metadata()
        {
            var appRequest = AppRequests.Boilerplate();
            appRequest.SessionId = "sid";
            appRequest.ResponseId = "rid";
            appRequest.Result.LanguageCode = "en-US";
            appRequest.Result.Intent.DisplayName = "intent";
            appRequest.OriginalDetectIntentRequest.Content.User.UserId = "uid";
            var context = new ConversationContext();
            new ActionInputModelBuilder().Build(context, appRequest);
            Assert.Equal("sid", context.RequestModel.SessionId);
            Assert.Equal("rid", context.RequestModel.RequestId);
            Assert.Equal("en-US", context.RequestModel.Locale);
            Assert.Equal("uid", context.RequestModel.UserId);
            Assert.Equal("intent", context.RequestModel.IntentName);
            Assert.Equal(RequestType.Intent, context.RequestType);
        }

        [Fact]
        public void Parameters()
        {
            var appRequest = AppRequests.Boilerplate();
            appRequest.Result.Parameters["p1"] = "v1";
            appRequest.Result.Parameters["p2"] = "v2";
            var context = new ConversationContext();
            new ActionInputModelBuilder().Build(context, appRequest);
            Assert.Equal("v1", context.RequestModel.GetParameterValue("p1"));
            Assert.Equal("v2", context.RequestModel.GetParameterValue("p2"));
        }

        [Fact]
        public void SessionIsRestored()
        {
            var session = new Dictionary<string, string> {["s1"] = "v1", ["s2"] = "v2"};
            var sessionJson = JsonConvert.SerializeObject(session);
            var appRequest = AppRequests.Boilerplate();
            appRequest.OriginalDetectIntentRequest.Content.User.UserStorage = sessionJson;
            var context = new ConversationContext();
            new ActionInputModelBuilder().Build(context, appRequest);
            Assert.Equal("v1", context.SessionValues["s1"]);
            Assert.Equal("v2", context.SessionValues["s2"]);
        }

        private static AppRequest CreateRequestWithIntent(string intentName)
        {
            var request = AppRequests.Boilerplate();
            var input = new Input
            {
                Intent = intentName,
                RawInputs = new List<RawInput>(),
                Arguments = new List<Argument>()
            };
            request.Result.Intent.DisplayName = intentName;
            request.OriginalDetectIntentRequest.Content.Inputs.Add(input);
            return request;
        }
    }
}