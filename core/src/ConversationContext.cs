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
            this.SessionValues = new Dictionary<string, string>();
            this.Extensions = new ExtensionModel();
        }
        
        /// <summary>
        /// Model describing the current request
        /// </summary>
        public RequestModel RequestModel { get; set; }
        
        /// <summary>
        /// Describes device capabilities
        /// </summary>
        public IEnumerable<DeviceCapability> Capabilities { get; set; }
        
        /// <summary>
        /// Virtual directives describing what to send back to the user
        /// </summary>
        public List<IVirtualDirective> OutputDirectives { get; }
        
        /// <summary>
        /// Extension Model
        /// </summary>
        public ExtensionModel Extensions { get; }
        
        /// <summary>
        /// Session variables (persisted across requests)
        /// </summary>
        public Dictionary<string, string> SessionValues { get; }
        
        /// <summary>
        /// Type of request
        /// </summary>
        public RequestType RequestType { get; set; }
    }
}