using System;

namespace VoiceBridge.Most.Logging
{
    public interface IMetricsReporter
    {
        void Increment(string metricName);
        void ReportTime(string metricName, TimeSpan duration);
        void ReportValue(string metricName, int value);
    }
}