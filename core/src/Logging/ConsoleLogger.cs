using System;

namespace VoiceBridge.Most.Logging
{
    public class ConsoleLogger : ILogger, IMetricsReporter
    {
        public void Log(LogLevel level, string message, params object[] formattingArgs)
        {
            var text = string.Format(message, formattingArgs);
            Console.WriteLine("[{0}]: {1}", level, text);
        }

        public void Increment(string metricName)
        {
            Console.WriteLine("[Metrics-Increment]: {0}", metricName);
        }

        public void ReportTime(string metricName, TimeSpan duration)
        {
            Console.WriteLine("[Metrics-Time: {0}]: {1}", metricName, duration);
        }

        public void ReportValue(string metricName, int value)
        {
            Console.WriteLine("[Metrics-Value: {0}]: {1}", metricName, value);
        }
    }
}