using System;

namespace VoiceBridge.Most.Logging
{
    public class NullLoggerReporter : ILogger, IMetricsReporter
    {
        public void Log(LogLevel level, string message, params object[] formattingArgs)
        {
        }

        public void Increment(string metricName)
        {
        }

        public void ReportTime(string metricName, TimeSpan duration)
        {
        }

        public void ReportValue(string metricName, int value)
        {
        }
    }
}