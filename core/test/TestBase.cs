using VoiceBridge.Most.Logging;
using Xunit.Abstractions;

namespace VoiceBridge.Most.Test
{
    public abstract class TestBase : ILogger
    {
        private readonly ITestOutputHelper output;

        protected TestBase(ITestOutputHelper output)
        {
            this.output = output;
        }

        protected void LogMessage(string message)
        {
            this.output.WriteLine(message);
        }

        public void Log(LogLevel level, string message, params object[] formattingArgs)
        {
            var msg = message;
            if (formattingArgs?.Length > 0)
            {
                msg = string.Format(message, formattingArgs);
                msg = $"[{level}] {msg}";
            }

            this.LogMessage(msg);
        }
    }
}