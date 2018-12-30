using System;
using VoiceBridge.Most.VoiceModel;

namespace VoiceBridge.Most
{
    public class Media
    {
        public Media()
        {
            this.Token = Guid.NewGuid().ToString("N");
        }
        
        public Uri StreamUrl { get; set; }
        
        public string Title { get; set; }
        
        public string Subtitle { get; set; }
        
        public string Author { get; set; }
        
        public Uri LargeImageUrl { get; set; }
        
        public Uri SmallImageUrl { get; set; }
        
        public string Token { get; set; }
    }
}