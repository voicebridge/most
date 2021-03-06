﻿using System;
using System.Collections.Generic;
using System.Text;

namespace VoiceBridge.Common.Extensions
{
    /// <summary>
    /// Extenstions methods for System.Uri
    /// </summary>
    public static class UriExtensions
    {
        /// <summary>
        /// Convert this Uri to a SecureUrl
        /// </summary>
        public static SecureUrl ToSecure(this Uri uri)
        {
            return new SecureUrl(uri);
        }
    }
}
