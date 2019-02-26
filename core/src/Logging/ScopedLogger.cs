using System;

namespace VoiceBridge.Most.Logging
{
    internal class ScopedLogger : ILogger
    {
        private readonly ILogger logger;

        public ScopedLogger(ILogger logger)
        {
            this.logger = logger;
        }
        
        public ConversationContext CurrentContext { get; set; }

        private string Prefix
        {
            get
            {
                var value = "[MOST] ";
                if (this.CurrentContext?.RequestModel == null) return value;
                
                value += $"[MOST_rid:{this.CurrentContext.RequestModel.RequestId}] ";
                value += $"[MOST_uid:{this.CurrentContext.RequestModel.UserId}] ";
                value += $"[MOST_sid:{this.CurrentContext.RequestModel.SessionId}] ";

                return value;
            }
        }
        
        public void Log(LogLevel level, string message, params object[] formattingArgs)
        {
            var msg = this.Prefix + message;
            try
            {
                this.logger.Log(level, msg, formattingArgs);
            }
            catch
            {
                //Logging should not prevent the request from completing
            }
        }
    }
}