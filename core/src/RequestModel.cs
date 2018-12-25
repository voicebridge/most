using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

namespace VoiceBridge.Most
{
    public class RequestModel
    {
        public RequestModel()
        {
            this.Parameters = new Dictionary<string, ParameterValue>();
        }
        
        public string IntentName { get; set; }
        public string SessionId { get; set; }
        public string UserId { get; set; }
        public string RequestId { get; set; }
        public string Locale { get; set; }
        
        public Dictionary<string, ParameterValue> Parameters { get; }

        public bool ParameterHasValue(string name)
        {
            return this.Parameters.ContainsKey(name) && !string.IsNullOrWhiteSpace(this.Parameters[name].ResolvedId);
        }

        public string GetParameterValue(string name)
        {
            return this.ParameterHasValue(name) ? this.Parameters[name].ResolvedId : null;
        }
    }
}