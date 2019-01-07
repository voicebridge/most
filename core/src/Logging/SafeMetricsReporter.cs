using System;

namespace VoiceBridge.Most.Logging
{
    public class SafeMetricsReporter : IMetricsReporter
    {
        private readonly IMetricsReporter reporter;
        private readonly ILogger logger;

        public SafeMetricsReporter(IMetricsReporter reporter, ILogger logger)
        {
            this.reporter = reporter;
            this.logger = logger;
        }
        
        public void Increment(string metricName)
        {
            this.SafeExecute(r => r.Increment(metricName));
        }

        public void ReportTime(string metricName, TimeSpan duration)
        {
            this.SafeExecute(r => r.ReportTime(metricName, duration));
        }

        public void ReportValue(string metricName, int value)
        {
            this.SafeExecute(r => r.ReportValue(metricName, value));
        }

        private void SafeExecute(Action<IMetricsReporter> action)
        {
            if (this.reporter == null)
            {
                this.logger.Debug("Metrics reporting skipped. Reporter is null");
                return;
            }

            try
            {
                action(this.reporter);
            }
            catch (Exception exception)
            {
                this.logger.Error($"Metrics reporter is failing to log. Error: {exception}");
            }
        }
    }
}