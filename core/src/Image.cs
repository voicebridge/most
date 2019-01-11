using System;
using System.Collections.Generic;
using System.Text;
using VoiceBridge.Common;

namespace VoiceBridge.Most
{
    /// <summary>
    /// Represents a static image to be displayed on-device (if supported)
    /// </summary>
    public class Image : IImage
    {
        // public string AccessibleText { get; set; }
        // public string Caption { get; set; }

        public SecureUrl ImageUri { get; set; }
    }
}
