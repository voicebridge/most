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

        public SecureUri SmallImageUri { get; set; }

        public SecureUri MediumImageUri { get; set; }

        public SecureUri LargeImageUri { get; set; }

        public SecureUri ExtraLargeImageUri { get; set; }
    }
}