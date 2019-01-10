using System.Collections.Generic;
using VoiceBridge.Most.VoiceModel.Alexa;
using VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK;

namespace VoiceBridge.Most.Alexa
{
    internal static class Extensions
    {
        public static IOutputSpeech ToAlexaSpeech(this Prompt prompt)
        {
            return prompt.IsSSML
                ? (IOutputSpeech)new SSMLOutputSpeech {Content = prompt.Content}
                : new PlainTextOutputSpeech {Text = prompt.Content};
        }

        public static SimpleResponseItem ToAssistantSimpleResponse(this Prompt prompt)
        {
            return new SimpleResponseItem
            {
                Value = new SimpleResponse
                {
                    TextToSpeech = prompt.IsSSML ? null : prompt.Content,
                    SSML = prompt.IsSSML ? prompt.Content : null
                }
            };
        }
    }
}