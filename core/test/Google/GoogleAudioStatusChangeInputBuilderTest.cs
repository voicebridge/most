using VoiceBridge.Most.Google;
using VoiceBridge.Most.VoiceModel.GoogleAssistant.DialogFlow;
using Xunit;

namespace VoiceBridge.Most.Test.Google
{
    public class GoogleAudioStatusChangeInputBuilderTest
    {
        [Fact]
        public void VerifyStatusIsReadCorrectly()
        {
            var request = Files.GoogleMediaStatusRequest.FromJson<AppRequest>();
            var context = new ConversationContext();
            new GoogleAudioStatusChangeInputBuilder().Build(context, request);
            var audioInfo = context.Extensions.Get<AudioPlayerInfo>();
            Assert.Equal(AudioPlayerState.Finished, audioInfo.State);
            Assert.Null(audioInfo.CurrentOffsetInMilliseconds);
        }
        
        [Fact]
        public void MediaStatusFailedState()
        {
            var request = Files.GoogleMediaStatusRequest.FromJson<AppRequest>();
            request.OriginalDetectIntentRequest.Content.Inputs[0].Arguments[0].Extension.Status = "FAILED";
            var context = new ConversationContext();
            new GoogleAudioStatusChangeInputBuilder().Build(context, request);
            var audioInfo = context.Extensions.Get<AudioPlayerInfo>();
            Assert.Equal(AudioPlayerState.Failed, audioInfo.State);
            Assert.Null(audioInfo.CurrentOffsetInMilliseconds);
        }
    }
}