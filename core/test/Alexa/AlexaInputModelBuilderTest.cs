using System;
using System.Collections.Generic;
using System.Linq;
using VoiceBridge.Most.Alexa;
using VoiceBridge.Most.VoiceModel.Alexa;
using Xunit;

namespace VoiceBridge.Most.Test.Alexa
{
    public class AlexaInputModelBuilderTest
    {
        private const string RequestId = "req-id";
        private const string SessionId = "session-id";
        private const string UserId = "user-id";
        private const string IntentName = "some-intent";
        
        [Fact]
        public void RequestIdIsSet()
        {
            var context = BuildModel();
            Assert.Equal(RequestId, context.RequestModel.RequestId);
        }

        [Fact]
        public void SessionIdIsSet()
        {
            var context = BuildModel();
            Assert.Equal(SessionId, context.RequestModel.SessionId);
        }

        [Fact]
        public void UserIdIsSet()
        {
            var context = BuildModel();
            Assert.Equal(UserId, context.RequestModel.UserId);
        }

        [Fact]
        public void IntentNameIsSet()
        {
            var context = BuildModel();
            Assert.Equal(IntentName, context.RequestModel.IntentName);
        }

        [Fact]
        public void NonIntentRequest()
        {
            var request = CreateRequest();
            request.Content.Type = AlexaConstants.RequestType.SessionEndedRequest;
            request.Content.Intent = null;
            var context = BuildModel(request);
            Assert.Equal(AlexaConstants.RequestType.SessionEndedRequest, context.RequestModel.IntentName);
        }

        [Fact]
        public void SlotWithNoCanonicalValue()
        {
            var request = CreateRequest();
            request.Content.Intent.Slots["p1"] = new Slot
            {
                Name = "p1",
                Value = "v1"
            };
            var context = BuildModel(request);           
            var param = context.RequestModel.Parameters["p1"];
            Assert.Equal("v1", context.RequestModel.GetParameterValue("p1"));
            Assert.Equal("v1", param.ProvidedValue);
            Assert.Equal("v1", param.ResolvedId);
            Assert.Equal("v1", param.ResolvedValue);
        }

        [Fact]
        public void SlotWithCannonicalValueMatchButValueIsNull()
        {
            //Real use case which was encountered in Alexa
            throw new Exception("fix me!");
        }
        
        [Fact]
        public void SlotWithCanonicalValue()
        {
            const string name = "p1";
            const string id = "6472353";
            const string resolvedValue = "v1";
            const string spokenValue = "value one";
            
            var request = CreateRequest();
            request.Content.Intent.Slots["p1"] = 
                BuildSlot(
                name,
                spokenValue,
                AlexaConstants.SlotResolutionStatus.SuccessfulMatch,
                resolvedValue,
                id);
            
            var context = BuildModel(request);                   
            var param = context.RequestModel.Parameters["p1"];
            Assert.Equal(id, context.RequestModel.GetParameterValue("p1"));
            Assert.Equal(resolvedValue, param.ResolvedValue);
            Assert.Equal(id, param.ResolvedId);
            Assert.Equal(spokenValue, param.ProvidedValue);
        }
        
        [Fact]
        public void SlotWithNoCanonicalValueMatch()
        {
            const string name = "p1";
            const string spokenValue = "value one";
            
            var request = CreateRequest();
            request.Content.Intent.Slots["p1"] = 
                BuildSlot(
                    name,
                    spokenValue,
                    AlexaConstants.SlotResolutionStatus.FailedMatch);
            
            var context = BuildModel(request);                   
            var param = context.RequestModel.Parameters["p1"];
            Assert.Equal(spokenValue, context.RequestModel.GetParameterValue("p1"));
            Assert.Equal(spokenValue, param.ResolvedValue);
            Assert.Equal(spokenValue, param.ResolvedId);
            Assert.Equal(spokenValue, param.ProvidedValue);
        }

        private static Slot BuildSlot(
            string name,
            string spokenValue,
            string resolutionCode = null,
            string resolvedValue = null,
            string resolvedId = null)
        {
            var slot = new Slot
            {
                Name = name,
                Value = spokenValue,
                Resolutions = new SlotResolutions
                {
                    ResolutionsByAuthority = new List<SlotResolutionPerAuthority>()
                    {
                        new SlotResolutionPerAuthority
                        {
                            Status = new SlotResolutionStatus
                            {
                                Code = resolutionCode
                            },
                            Values = new List<SlotResolutionValue>
                            {
                                new SlotResolutionValue
                                {
                                    Name = resolvedValue,
                                    Id = resolvedId
                                }
                            }
                            
                        }
                    }
                }
            };

            if (resolutionCode == null)
            {
                slot.Resolutions = null;
            }

            if (resolutionCode != null && resolvedId == null)
            {
                slot.Resolutions.ResolutionsByAuthority.First().Values = null;
            }

            return slot;
        }

        private static ConversationContext BuildModel(
            SkillRequest request = null)
        {
            if (request == null)
            {
                request = CreateRequest();
            }
            
            var context = new ConversationContext();
            var builder = new AlexaInputModelBuilder();
            builder.Build(context, request);
            return context;
        }

        private static SkillRequest CreateRequest()
        {
            var request = new SkillRequest
            {
                Content = new RequestContent
                {
                    Type = AlexaConstants.RequestType.IntentRequest,
                    RequestId = RequestId,
                    Intent = new Intent
                    {
                        Name = IntentName,
                        Slots = new Dictionary<string, Slot>()
                    }
                },
                Session = new Session
                {
                    User = new UserInfo {UserId = UserId},
                    SessionId = SessionId
                }
            };

            return request;
        }
    }
}