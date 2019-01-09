using System.Reflection;
using VoiceBridge.Common.Extensions;

namespace VoiceBridge.Most.VoiceModel.Test
{
    public static class Files
    {
        private const string Prefix = "VoiceBridge.Most.VoiceModel.Test.TestFiles.";
        
        public static string SampleAlexaRequest => GetFile("SampleAlexaRequest.json");
        
        public static string SampleAlexaResponse=> GetFile("SampleAlexaResponse.json");

        public static string ActionSDKSimpleResponse => GetFile("ActionSDKSimpleResponse.json");

        public static string SampleActionSDKRequest => GetFile("SampleActionRequest.json");

        public static string SampleDialogFlowRequest => GetFile("DialogFlowRequest.json");

        public static string AlexaSupportedInterfacesTestCase => GetFile("AlexaSupportedInterfacesTest.json");

        private static string GetFile(string name)
        {
            return Assembly.GetExecutingAssembly().GetTextFile(Prefix + name);
        }
    }
}