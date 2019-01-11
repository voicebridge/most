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
        /// Convert the contents of this string to a SecureUrl
        /// </summary>
        public static SecureUrl ToSecureUrl(this string text)
        {
            return new SecureUrl(text);
        }
    }
}
