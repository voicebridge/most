using System.Reflection;
using VoiceBridge.Common.Extensions;

namespace VoiceBridge.Most.Test
{
    public static class Files
    {
        private const string Prefix = "VoiceBridge.Most.Test.TestFiles.";
        
        public static string HockeyScoreIntentRequest => GetFile("HockeyScoreIntentRequest.json");
        
        public static string GoogleImplicitRequestSample => GetFile("GoogleImplicitRequestSample.json");

        public static string AlexaPlaybackFinished => GetFile("AlexaPlaybackFinishedJson.json");

        public static string GoogleNoInputRequest => GetFile("GoogleNoInputRequest.json");

        public static string APLUserEventRequest => GetFile("APLUserEventSample.json");

        public static string AlexaDisplayElementSelected => GetFile("DisplayElementSelectedSample.json");

        public static string GoogleOptionSelected => GetFile("GoogleOptionSelectSample.json");

        public static string GoogleMediaStatusRequest => GetFile("GoogleMediaStatus.json");

        private static string GetFile(string name)
        {
            return Assembly.GetExecutingAssembly().GetTextFile(Prefix + name);
        }
    }
}