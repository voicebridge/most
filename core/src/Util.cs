using System;
using Newtonsoft.Json;
using VoiceBridge.Most.Logging;

namespace VoiceBridge.Most
{
    internal static class Util
    {
        public static void AssertNotNull<T>(T obj, string name)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(name);
            }
        }

        public static void Info(this ILogger logger, string message)
        {
            logger.Log(LogLevel.Information, message);
        }

        public static void Debug(this ILogger logger, string message)
        {
            logger.Log(LogLevel.Debug, message);
        }
        
        public static void Error(this ILogger logger, string message)
        {
            logger.Log(LogLevel.Error, message);
        }

        public static string ToJson<T>(this T obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static bool StringOrdinalEquals(string a, string b)
        {
            return string.Compare(a, b, StringComparison.InvariantCultureIgnoreCase) == 0;
        }
    }
}