using System;
using System.Threading;
using VoiceBridge.Most.Logging;
using Xunit;

namespace VoiceBridge.Most.Test.Logging
{
    public class TimedActionTest
    {
        [Fact]
        public void MeasureTime()
        {
            const string name = "fake";
            var fakeReporter = new FakeReporter();
            using (fakeReporter.MeasureTime(name))
            {
                Thread.Sleep(100);
            }
            Assert.True(fakeReporter.Duration >= TimeSpan.FromMilliseconds(100));
            Assert.Equal(name, fakeReporter.MetricName);
        }
        
        private class FakeReporter : IMetricsReporter
        {
            public string MetricName;
            public TimeSpan Duration = TimeSpan.Zero;
            
            public void Increment(string metricName)
            {
                throw new NotImplementedException();
            }

            public void ReportTime(string metricName, TimeSpan duration)
            {
                this.MetricName = metricName;
                this.Duration = duration;
            }

            public void ReportValue(string metricName, int value)
            {
                throw new NotImplementedException();
            }
        }
    }
}