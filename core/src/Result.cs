using VoiceBridge.Most.Directives;

namespace VoiceBridge.Most
{
    public static class Result
    {
        public static IVirtualDirective Say(string plainTextPrompt, bool keepSessionOpen = true)
        {
            return Say(plainTextPrompt.AsPrompt(), keepSessionOpen); 
        }

        public static IVirtualDirective Say(Prompt prompt, bool keepSessionOpen = true)
        {
            return new SayDirective
            {
                Prompt = prompt,
                KeepSessionOpen = keepSessionOpen
            };
        }

        public static IVirtualDirective Ask(string plainTextPrompt, string parameterName, string expectedIntentName = null)
        {
            return Ask(plainTextPrompt.AsPrompt(), parameterName, expectedIntentName);
        }

        public static IVirtualDirective Ask(Prompt prompt, string parameterName, string expectedIntentName = null)
        {
            return new AskForValueDirective
            {
                ParameterName = parameterName,
                Prompt = prompt,
                ExpectedIntentName = expectedIntentName
            };
        }

        public static IVirtualDirective PlayAudio(Media media, bool keepSessionOpen = false)
        {
            return new PlayMediaDirective(media)
            {
                ResponseExpected = keepSessionOpen
            };
        }
    }
}