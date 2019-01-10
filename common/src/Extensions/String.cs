using System;
using System.Collections.Generic;
using System.Text;

namespace VoiceBridge.Common.Extensions
{
    /// <summary>
    /// Extension methods for System.String
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Convert the contents of this string to a Secure Uri
        /// </summary>
        public static SecureUri ToSecureUri(this string text)
        {
            return new SecureUri(text);
        }
    }
}
