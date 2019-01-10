using VoiceBridge.Most.Alexa;
using VoiceBridge.Most.VoiceModel.Alexa;
using Xunit;

namespace VoiceBridge.Most.Test.Alexa
{
    public class AlexaAudioStatusChangeInputBuilderTest
    {
        [Fact]
        public void PlayingState()
        {
            var info = RunSimulation(AlexaConstants.AudioPlayer.RequestTypes.Started);
            Assert.NotNull(info);
            Assert.Equal(AudioPlayerState.Active, info.State);
            Assert.Equal(8047, info.CurrentOffsetInMilliseconds);
        }
        
        
        [Fact]
        public void VerifyStateMapping()
        {
            AssertStateMapping(AudioPlayerState.Active, AlexaConstants.AudioPlayer.RequestTypes.Started);
            AssertStateMapping(AudioPlayerState.Failed, AlexaConstants.AudioPlayer.RequestTypes.Failed);
            AssertStateMapping(AudioPlayerState.StoppedByUser, AlexaConstants.AudioPlayer.RequestTypes.Stopped);
            AssertStateMapping(AudioPlayerState.Finished, AlexaConstants.AudioPlayer.RequestTypes.Finished);
            AssertStateMapping(AudioPlayerState.Finishing, AlexaConstants.AudioPlayer.RequestTypes.Finishing);
        }

        private static void AssertStateMapping(AudioPlayerState state, string requestType)
        {
            Assert.Equal(state, RunSimulation(requestType).State);
        }

        private static AudioPlayerInfo RunSimulation(string state)
        {
            var request = Files.AlexaPlaybackFinished.FromJson<SkillRequest>();
            request.Content.Type = $"{AlexaConstants.AudioPlayer.RequestTypePrefix}{state}";
            var context = new ConversationContext();
            var inputBuilder = new AlexaAudioStatusChangeInputBuilder();
            inputBuilder.Build(context, request);
            return context.Extensions.Get<AudioPlayerInfo>();
        }
    }
}