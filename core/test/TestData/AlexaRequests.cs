using System;
using System.Collections.Generic;
using VoiceBridge.Most.VoiceModel.Alexa;

namespace VoiceBridge.Most.Test.TestData
{
    public static class AlexaRequests
    {
        public static SkillRequest Weather(string city)
        {
            var request = CreateBoilerplateRequest();
            request.Content.Intent.Name = TestIntents.Weather;
            request.Content.Intent.Slots["city"] = new Slot
            {
                Name = "city",
                Value = city
            };
            return request;
        }

        public static SkillRequest CreateBoilerplateRequest()
        {
            var user = CreateUser();
            var app = CreateAppInfo();
            var request = new SkillRequest
            {
                Version = AlexaConstants.AlexaVersion,
                Session = CreateSession(user, app),
                Context = CreateContext(user, app),
                Content = CreateContent()
            };
            return request;
        }

        public static RequestContent CreateContent()
        {
            var content = new RequestContent
            {
                RequestId = Known.RequestId,
                Type = AlexaConstants.RequestType.IntentRequest,
                Intent = new Intent
                {
                    Slots = new Dictionary<string, Slot>()
                }
            };
            return content;
        }

        public static RequestContext CreateContext(UserInfo user, ApplicationInfo app)
        {
            var context = new RequestContext
            {
                System = new AlexaInfo
                {
                    User = user, 
                    Application = app
                }
            };
            return context;
        }

        public static Session CreateSession(UserInfo user, ApplicationInfo app)
        {
            var session = new Session
            {
                User = user,
                Attributes = new Dictionary<string, string>(),
                SessionId = Generic.Id(),
                Application = app
            };
            return session;
        }

        public static UserInfo CreateUser()
        {
            return new UserInfo
            {
                UserId = Known.UserId,
                AccessToken = Generic.Id(),
                Permissions = new Permissions
                {
                    ConsentToken = Generic.Id()
                }
            };
        }

        public static ApplicationInfo CreateAppInfo()
        {
            return new ApplicationInfo
            {
                ApplicationId = Known.ApplicationId
            };
        }
    }
}