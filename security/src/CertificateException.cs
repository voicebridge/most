using System;
using System.Collections.Generic;
using System.Text;

namespace VoiceBridge.Most.Security
{
    /// <summary>
    /// An Exception that occurs when dealing with Certificates
    /// </summary>
    public class CertificateException : Exception
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public CertificateException(string message)
            : base(message)
        {
        }
    }
}
