using System;
using System.Collections.Generic;
using System.Text;
using VoiceBridge.Common;

namespace VoiceBridge.Most
{
    /// <summary>
    /// Represents a collection of images to be displayed on-device, by form (if supported)
    /// </summary>
    public class ResponsiveImage : IImage
    {
        // public string AccessibleText { get; set; }
        // public string Caption { get; set; }

        public SecureUrl SmallImageUri { get; set; }

        public SecureUrl MediumImageUri { get; set; }

        public SecureUrl LargeImageUri { get; set; }

        public SecureUrl ExtraLargeImageUri { get; set; }
    }
}