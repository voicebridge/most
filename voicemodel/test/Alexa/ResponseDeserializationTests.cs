using System.Collections.Generic;
using Newtonsoft.Json;
using VoiceBridge.Most.VoiceModel.Alexa;
using VoiceBridge.Most.VoiceModel.Alexa.Directives;
using Xunit;

namespace VoiceBridge.Most.VoiceModel.Test.Alexa
{
    public class ResponseDeserializationTests
    {
        [Fact]
        public void VerifyResponseSerialization()
        {
            var updatedIntent = new Intent
            {
                ConfirmationStatus = "NONE",
                Name = "PlaceOrderIntent",
                Slots = new Dictionary<string, Slot>
                {
                    {
                        "drink_size", new Slot
                        {
                            Name = "drink_size",
                            ConfirmationStatus = AlexaConstants.Dialog.SlotStatus.None
                        }
                    }
                }
            };
            var response = new SkillResponse
            {
                Version = AlexaConstants.AlexaVersion,
                SessionAttributes = new Dictionary<string, string> {{"turn_index", "1"}},
                Content = new ResponseContent
                {
                    Directives =
                        new List<IAlexaDirective>
                        {
                            new ElicitSlotDialogDirective("drink_size")
                            {
                                UpdatedIntent = updatedIntent
                            }
                        },
                    ShouldEndSession = false,
                    OutputSpeech = new PlainTextOutputSpeech
                    {
                        Text = "Hello! What can I get started for you?"
                    }
                }
            };
            var json = Serializer.SerializeResponseWithFormatting(response);
            Assert.Equal(Files.SampleAlexaResponse, json);
        }
    }
}