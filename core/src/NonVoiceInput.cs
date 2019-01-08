using System.Collections.Generic;

namespace VoiceBridge.Most
{
    public class NonVoiceInput
    {
        public string SourceName { get; set; }
        
        public IEnumerable<string> Values { get; set; }
    }
}