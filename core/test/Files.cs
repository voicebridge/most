using System.Reflection;
using VoiceBridge.Common.Extensions;

namespace VoiceBridge.Most.Test
{
    public static class Files
    {
        private const string Prefix = "VoiceBridge.Most.Test.TestFiles.";
        
        public static string HockeyScoreIntentRequest => GetFile("HockeyScoreIntentRequest.json");

        private static string GetFile(string name)
        {
            return Assembly.GetExecutingAssembly().GetTextFile(Prefix + name);
        }
    }
}