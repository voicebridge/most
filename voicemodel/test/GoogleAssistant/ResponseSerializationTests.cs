using Newtonsoft.Json;
using VoiceBridge.Most.VoiceModel.GoogleAssistant;
using VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK;
using Xunit;

namespace VoiceBridge.Most.VoiceModel.Test.GoogleAssistant
{
    public class ResponseSerializationTests
    {
        [Fact]
        public void VerifyUseOfConverter()
        {
            var response = JsonConvert.DeserializeObject<ActionResponse>(Files.ActionSDKSimpleResponse);
            Assert.Equal("Howdy!", ((SimpleResponseItem)response.ExpectedInputs[0].InputPrompt.RichInitialPrompt.Items[0]).Value.TextToSpeech);
        }
    }
}