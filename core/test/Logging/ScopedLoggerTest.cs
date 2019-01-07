
using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using VoiceBridge.Most.Logging;
using Xunit;

namespace VoiceBridge.Most.Test.Logging
{
    public class ScopedLoggerTest
    {
        private const string Message = "message";
        
        [Fact]
        public void LogNoContextSet()
        {
            var fakeLogger = new FakeLogger();
            const string expectedLogMessage = "[MOST] " + Message;
            var logger = new ScopedLogger(fakeLogger);
            logger.Log(LogLevel.Debug, Message);
            Assert.True(fakeLogger.Items.Any(x => x.Item1 == LogLevel.Debug && x.Item2 == expectedLogMessage));

        }
        
        [Fact]
        public void LogFailureDoesNotPropage()
        {
            var mock = new Mock<ILogger>();
            mock.Setup(x => x.Log(It.IsAny<LogLevel>(), It.IsAny<string>(), It.IsAny<object[]>()))
                .Throws(new Exception());
            
            var logger = new ScopedLogger(mock.Object);
            logger.Log(LogLevel.Debug, Message);
        }
        
        [Fact]
        public void LogWithContextSet()
        {
            const string sessionId = "1";
            const string requestId = "2";
            const string userId = "3";
            
            var fakeLogger = new FakeLogger();
            const string expectedLogMessage = "[MOST] [MOST_rid:" + requestId + "] [MOST_uid:" + userId + "] [MOST_sid:" + sessionId + "] " + Message;
            
            var logger = new ScopedLogger(fakeLogger);
            logger.CurrentContext = new ConversationContext
            {
                RequestModel =
                {
                    RequestId = requestId,
                    UserId = userId,
                    SessionId = sessionId
                }
            };
            
            logger.Log(LogLevel.Debug, Message);
            Assert.True(fakeLogger.Items.Any(x => x.Item1 == LogLevel.Debug && x.Item2 == expectedLogMessage));

        }

        private class FakeLogger : ILogger
        {
            public readonly List<Tuple<LogLevel, string>> Items = new List<Tuple<LogLevel, string>>();
            
            public void Log(LogLevel level, string message, params object[] formattingArgs)
            {
                this.Items.Add(new Tuple<LogLevel, string>(level, message));
            }
        }
    }
}