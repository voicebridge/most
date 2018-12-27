using System.Collections.Generic;

namespace VoiceBridge.Most
{
    /// <summary>
    /// Contains information about the current request
    /// </summary>
    public class ConversationContext
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ConversationContext()
        {
            this.OutputDirectives = new List<IVirtualDirective>();
            this.RequestModel = new RequestModel();
            this.SessionStore = new Dictionary<string, string>();
        }
        
        /// <summary>
        /// Model describing the current request
        /// </summary>
        public RequestModel RequestModel { get; set; }
        
        /// <summary>
        /// Virtual directives describing what to send back to the user
        /// </summary>
        public List<IVirtualDirective> OutputDirectives { get; }
        
        /// <summary>
        /// Session variables (persisted across requests)
        /// </summary>
        public Dictionary<string, string> SessionStore { get; }
        
        /// <summary>
        /// Type of request
        /// </summary>
        public RequestType RequestType { get; set; }
    }
}