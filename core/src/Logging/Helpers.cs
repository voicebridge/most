using System;

namespace VoiceBridge.Most.Logging
{
    public static class Helpers
    {
        public static IDisposable MeasureTime(this IMetricsReporter reporter, string metricName)
        {
            return new TimedAction(reporter, metricName);
        }
    }
}