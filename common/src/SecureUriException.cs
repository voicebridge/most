using System;
using System.Collections.Generic;
using System.Text;

namespace VoiceBridge.Common
{
    /// <summary>
    /// An Exception that occurs when constructing a SecureUri
    /// </summary>
    public class SecureUriException : Exception
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public SecureUriException(string message)
            : base(message)
        {
        }
    }
}
