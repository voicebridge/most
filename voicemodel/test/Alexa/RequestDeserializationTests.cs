using System.Linq;
using Newtonsoft.Json.Serialization;
using VoiceBridge.Most.VoiceModel.Alexa;
using Xunit;

namespace VoiceBridge.Most.VoiceModel.Test.Alexa
{
    public class RequestDeserializationTests
    {
        [Fact]
        public void TopLevelProperties()
        {
            var request = GetTestRequest();
            Assert.Equal(AlexaConstants.AlexaVersion, request.Version);
        }

        [Fact]
        public void SessionObject()
        {
            var session = GetTestRequest().Session;
            Assert.True(session.New);
            Assert.Equal("session_id", session.SessionId);
            Assert.Equal("app_id", session.Application.ApplicationId);
            Assert.Equal("value1", session.Attributes["key1"]);
            Assert.Equal("value2", session.Attributes["key2"]);
            Assert.Equal("user_id", session.User.UserId);
            Assert.Equal("access_token", session.User.AccessToken);
            Assert.Equal("consent_token", session.User.Permissions.ConsentToken);
        }

        [Fact]
        public void ContextSystemObject()
        {
            var system = GetTestRequest().Context.System;
            Assert.Equal("device_id", system.Device.DeviceId);
            Assert.True(system.Device.SupportedInterfaces.ContainsKey("AudioPlayer"));
            Assert.Equal("user_id2", system.User.UserId);
            Assert.Equal("access_token2", system.User.AccessToken);
            Assert.Equal("consent_token2", system.User.Permissions.ConsentToken);
            Assert.Equal("app_id_2", system.Application.ApplicationId);
            Assert.Equal("api_url", system.ApiEndpoint);
            Assert.Equal("api_token", system.ApiAccessToken);
        }

        [Fact]
        public void ContextAudioPlayer()
        {
            var player = GetTestRequest().Context.AudioPlayer;
            Assert.Equal(AlexaConstants.AudioPlayerStatus.Playing,
                player.Activity);
            Assert.Equal("audioplayer-token", player.Token);
            Assert.Equal(5, player.OffsetInMilliseconds);
        }

        [Fact]
        public void ContextSystemViewPort()
        {
            var viewPort = GetTestRequest().Context.System.ViewPort;
            Assert.Equal(246, viewPort.Experiences.Single().ArcMinuteWidth);
            Assert.Equal(144, viewPort.Experiences.Single().ArcMinuteHeight);
            Assert.True(viewPort.Experiences[0].CanResize);
            Assert.False(viewPort.Experiences[0].CanRotate);
            Assert.Equal("RECTANGLE", viewPort.Shape);
            Assert.Equal(1024, viewPort.PixelWidth);
            Assert.Equal(600, viewPort.PixelHeight);
            Assert.Equal(600, viewPort.CurrentPixelHeight);
            Assert.Equal(1024, viewPort.CurrentPixelWidth);
            Assert.Equal("SINGLE", viewPort.TouchModes.Single());
        }

        private static SkillRequest GetTestRequest()
        {
            return Serializer.DeserializeRequest(Files.SampleAlexaRequest);
        }
    }
}