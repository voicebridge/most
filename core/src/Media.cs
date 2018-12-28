using System;
using VoiceBridge.Most.VoiceModel;

namespace VoiceBridge.Most
{
    public class Media
    {
        public Uri StreamUrl { get; set; }
        
        public string Title { get; set; }
        
        public string Author { get; set; }
        
        public Uri ArtUrl { get; set; }
        
        public string Token { get; set; }
    }
}