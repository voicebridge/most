using System;
using System.Collections.Generic;
using System.Text;

namespace VoiceBridge.Most.Directives
{
    public class ImageDirective : IVirtualDirective
    {
        public Uri Image { get; set; }

        public string AccessibilityText { get; set; }

        public bool KeepSessionOpen { get; set; }
    }
}
