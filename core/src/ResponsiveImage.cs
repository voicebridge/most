using System;
using System.Collections.Generic;
using System.Text;

namespace VoiceBridge.Most
{
    /// <summary>
    /// Represents a collection of images to be displayed on-device, by form (if supported)
    /// </summary>
    public class ResponsiveImage : IImage
    {
        // public string AccessibleText { get; set; }
        // public string Caption { get; set; }

        public Uri SmallImageUri { get; set; }

        public Uri MediumImageUri { get; set; }

        public Uri LargeImageUri { get; set; }

        public Uri ExtraLargeImageUri { get; set; }
    }
}