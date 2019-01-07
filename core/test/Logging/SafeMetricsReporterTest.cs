using System;
using Moq;
using VoiceBridge.Most.Logging;
using Xunit;
using Xunit.Abstractions;

namespace VoiceBridge.Most.Test.Logging
{
    public class SafeMetricsReporterTest : TestBase
    {
        [Fact]
        public void IncrementDoesNotFail()
        {
            GetReporter().Increment("aaa");
        }

        [Fact]
        public void ReportTimeDoesNotFail()
        {
            GetReporter().ReportTime("bbb", TimeSpan.MaxValue);
        }

        [Fact]
        public void ReportValueDoesNotFail()
        {
            GetReporter().ReportValue("ccc", 33);
        }


        private IMetricsReporter GetReporter()
        {
            var mock = new Mock<IMetricsReporter>(MockBehavior.Strict);
            return new SafeMetricsReporter(mock.Object, this);
        }
        
        public SafeMetricsReporterTest(ITestOutputHelper output) : base(output)
        {
        }
    }
}