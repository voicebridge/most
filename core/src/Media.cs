using System;
using VoiceBridge.Common;
using VoiceBridge.Most.VoiceModel;

namespace VoiceBridge.Most
{
    /// <summary>
    /// Describes media object
    /// </summary>
    public class Media
    {
        public Media()
        {
            this.Token = Guid.NewGuid().ToString("N");
        }
        
        /// <summary>
        /// Media file Url (must be https)
        /// </summary>
        public SecureUri StreamUrl { get; set; }
        
        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; set; }
        
        /// <summary>
        /// Subtitle
        /// </summary>
        public string Subtitle { get; set; }
        
        /// <summary>
        /// Content Author
        /// </summary>
        public string Author { get; set; }
        
        /// <summary>
        /// Album Art Url
        /// </summary>
        public SecureUri LargeImageUrl { get; set; }
        
        /// <summary>
        /// Icon Album Art Url
        /// </summary>
        public SecureUri SmallImageUrl { get; set; }
        
        /// <summary>
        /// Opaque Token to identify content
        /// </summary>
        public string Token { get; set; }
    }
}