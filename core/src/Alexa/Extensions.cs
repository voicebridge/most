using System.Collections.Generic;
using VoiceBridge.Most.VoiceModel.Alexa;

namespace VoiceBridge.Most.Alexa
{
    internal static class Extensions
    {
        public static IOutputSpeech ToAlexaSpeech(this Prompt prompt)
        {
            return new PlainTextOutputSpeech
            {
                Text = prompt.Content
            };
        }
    }
}