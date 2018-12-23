using System.Collections.Generic;

namespace VoiceBridge.Most
{
    public class Prompt
    {
        public string Id { get; set; }
        
        public string Content { get; set; }
        
        public bool IsSSML { get; set; }
        
        public string Locale { get; set; }
    }
}