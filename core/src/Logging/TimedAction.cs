using System;
using System.Diagnostics;

namespace VoiceBridge.Most.Logging
{
    public class TimedAction : IDisposable
    {
        private readonly IMetricsReporter reporter;
        private readonly string metricName;
        private readonly Stopwatch stopwatch = new Stopwatch();

        public TimedAction(IMetricsReporter reporter, string metricName)
        {
            this.reporter = reporter;
            this.metricName = metricName;
            this.stopwatch.Start();
        }

        public void Dispose()
        {
            this.stopwatch.Stop();
            this.reporter.ReportTime(this.metricName, this.stopwatch.Elapsed);
        }
    }
}