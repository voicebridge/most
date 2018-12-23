using System;
using Newtonsoft.Json;
using VoiceBridge.Most.Logging;

namespace VoiceBridge.Most
{
    internal static class Util
    {
        public static Prompt AsPrompt(this string s)
        {
            return new Prompt
            {
                Id = "transient-prompt",
                Content = s,
                IsSSML = false
            };
        }

        public static void AssertNotNull<T>(T obj, string name)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(name);
            }
        }

        public static void Debug(this ILogger logger, string message)
        {
            logger.Log(LogLevel.Debug, message);
        }

        public static string ToJson<T>(this T obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}