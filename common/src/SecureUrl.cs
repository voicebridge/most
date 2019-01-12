using System;
using System.Collections.Generic;
using System.Text;

namespace VoiceBridge.Common
{
    /// <summary>
    /// An immutable, sealed, Uri class that enforces a secure scheme
    /// </summary>
    public sealed class SecureUrl : Uri
    {
        /// <summary>
        /// For LOCAL use ONLY!
        /// </summary>
        public static bool USE_UNSAFE_FOR_LOCAL_TESTING = false;


        /// <summary>
        /// Determines if this Uri is secure or not
        /// </summary>
        public bool IsSecure { get; private set; }


        /// <summary>
        /// Construct a secure url using the given string
        /// </summary>
        public SecureUrl(string uri)
            : base(uri)
        {
            var exception = Validate();
            IsSecure = (exception == null);

            if (!USE_UNSAFE_FOR_LOCAL_TESTING && exception != null)
                throw exception;
        }


        /// <summary>
        /// Construct a secure url using the given Uri
        /// </summary>
        public SecureUrl(Uri uri)
            : base(uri.ToString())
        {
            var exception = Validate();
            IsSecure = (exception == null);

            if (!USE_UNSAFE_FOR_LOCAL_TESTING && exception != null)
                throw exception;
        }


        /// <summary>
        /// Validates this Uri to determine it's security
        /// </summary>
        private SecureUrlException Validate()
        {
            if (!IsWellFormedOriginalString())
                return new SecureUrlException("Uri is not well formed");

            if(IsFile)
                return new SecureUrlException("Uri is a local file");

            if(IsLoopback)
                return new SecureUrlException("Uri is a loopback address");

            var scheme = base.Scheme.ToLowerInvariant();

            if (string.IsNullOrWhiteSpace(scheme) || !(scheme.Equals("https") || scheme.Equals("wss")))
                return new SecureUrlException("Protocol is insecure or unknown");

            if(!IsAbsoluteUri)
                return new SecureUrlException("Uri is not absolute");

            if (IsUnc)
                return new SecureUrlException("Uri is a UNC path");

            return null;
        }
    }
}
