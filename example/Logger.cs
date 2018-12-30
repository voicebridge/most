using System;
using System.Linq;
using Amazon.Lambda.Core;
using VoiceBridge.Most.Logging;

namespace Sample
{
    public class Logger : ILogger
    {
        public void Log(LogLevel level, string message, params object[] formattingArgs)
        {
            try
            {
                var msg = formattingArgs != null && formattingArgs.Any()
                    ? string.Format(message, formattingArgs)
                    : message;
                msg = $"[{level}] {msg}";
                LambdaLogger.Log(msg);
            }
            catch (Exception e)
            {
                LambdaLogger.Log($"Badly formatted message: {message} ERROR: {e}");
            }
        }
    }
}