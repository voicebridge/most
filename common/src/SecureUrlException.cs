using System;
using System.Collections.Generic;
using System.Text;

namespace VoiceBridge.Common
{
    /// <summary>
    /// An Exception that occurs when constructing a SecureUrl
    /// </summary>
    public class SecureUrlException : Exception
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public SecureUrlException(string message)
            : base(message)
        {
        }
    }
}
