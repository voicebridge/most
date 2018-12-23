using System.IO;
using System.Reflection;

namespace VoiceBridge.Common.Extensions
{
    public static class AssemblyExtensions
    {
        public static string GetTextFile(this Assembly assembly, string fullName)
        {
            using(var stream = assembly.GetManifestResourceStream(fullName))
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}