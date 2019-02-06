using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

namespace VoiceBridge.Most
{
    /// <summary>
    /// Platform-agnostic request model
    /// </summary>
    public class RequestModel
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public RequestModel()
        {
            this.Parameters = new Dictionary<string, ParameterValue>();
        }
        
        /// <summary>
        /// Gets or sets intent name
        /// </summary>
        public string IntentName { get; set; }
        
        /// <summary>
        /// Gets or sets session id
        /// </summary>
        public string SessionId { get; set; }
        
        /// <summary>
        /// Gets or sets a unique user identifier as provided by the platform
        /// </summary>
        public string UserId { get; set; }
        
        /// <summary>
        /// Gets or sets a unique identifier for the current request as provided by the platform
        /// </summary>
        public string RequestId { get; set; }
        
        /// <summary>
        /// Gets or sets the locale of the current request
        /// </summary>
        public string Locale { get; set; }
        
        /// <summary>
        /// Parameters (slots)
        /// </summary>
        public Dictionary<string, ParameterValue> Parameters { get; }

        /// <summary>
        /// Gets whether a parameter (slot) has a value set
        /// </summary>
        /// <param name="name">Parameter (slot) name</param>
        /// <returns>True if parameter has a value</returns>
        public bool ParameterHasValue(string name)
        {
            return this.Parameters.ContainsKey(name) && !string.IsNullOrWhiteSpace(this.Parameters[name].ResolvedId);
        }

        /// <summary>
        /// Gets parameter value if the parameter exists
        /// </summary>
        /// <param name="name">Parameter (slot) name</param>
        /// <returns>Value if parameter has a value otherwise null</returns>
        public string GetParameterValue(string name)
        {
            return this.ParameterHasValue(name) ? this.Parameters[name].ResolvedId : null;
        }
    }
}