using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Xunit;
using VoiceBridge.Most.Google;

namespace VoiceBridge.Most.Test.Google
{
    public class ActionResponseFactoryTest
    {
        [Fact]
        public void Create()
        {
            var context = new ConversationContext();
            var response = new ActionResponseFactory().Create(context);

            Assert.Empty(response.Messages);
            Assert.NotNull(response.Payload.Body);
            Assert.Empty(response.Payload.Body.RichResponse.Items);
            Assert.False(response.Payload.Body.ExpectUserResponse);
            Assert.Null(response.Payload.Body.UserStorage);
            Assert.False(response.Payload.Body.ExpectUserResponse);
        }

        [Fact]
        public void SessionIsTransferred()
        {
            var context = new ConversationContext();
            context.SessionValues["s1"] = "v1";
            context.SessionValues["s2"] = "v2";
            var response = new ActionResponseFactory().Create(context);
            var json = response.Payload.Body.UserStorage;
            var session = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            Assert.Equal("v1", session["s1"]);
            Assert.Equal("v2", session["s2"]);
        }
    }
}