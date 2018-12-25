using System.Collections.Generic;
using Newtonsoft.Json;
using VoiceBridge.Most.Google;
using VoiceBridge.Most.Test.TestData;
using Xunit;

namespace VoiceBridge.Most.Test.Google
{
    public class ActionInputModelBuilderTest
    {
        [Fact]
        public void Metadata()
        {
            var appRequest = AppRequests.CreateBoileRequest();
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
        }

        [Fact]
        public void Parameters()
        {
            var appRequest = AppRequests.CreateBoileRequest();
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
            var appRequest = AppRequests.CreateBoileRequest();
            appRequest.OriginalDetectIntentRequest.Content.User.UserStorage = sessionJson;
            var context = new ConversationContext();
            new ActionInputModelBuilder().Build(context, appRequest);
            Assert.Equal("v1", context.SessionStore["s1"]);
            Assert.Equal("v2", context.SessionStore["s2"]);
        }
    }
}