namespace VoiceBridge.Most.Logging
{
    public interface ILogger
    {
        void Log(LogLevel level, string message, params object[] formattingArgs);
    }
}