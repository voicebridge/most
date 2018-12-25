using System.Collections.Generic;

namespace VoiceBridge.Most
{
    public class ConversationContext
    {
        public ConversationContext()
        {
            this.OutputDirectives = new List<IVirtualDirective>();
            this.RequestModel = new RequestModel();
            this.SessionStore = new Dictionary<string, string>();
        }
        
        public RequestModel RequestModel { get; set; }
        
        public List<IVirtualDirective> OutputDirectives { get; }
        
        public Dictionary<string, string> SessionStore { get; }
    }
}