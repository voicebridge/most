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
        /// A list of known protocols that use SSL/TLS
        /// Source: https://www.iana.org/assignments/uri-schemes/uri-schemes.xhtml
        /// </summary>
        private readonly IList<string> allowed = new List<string>()
        {
            "https",            // HTTP over SSL
            "wss"               // Secure web sockets

            //"aaas",
            //"ftps",
            //"hxxps",
            //"ipps",
            //"msrps",
            //"rtsps",
            //"snews",
        };


        /// <summary>
        /// Construct a secure url using the given string
        /// </summary>
        public SecureUrl(string uri)
            : base(uri)
        {
            var exception = Validate();
            IsSecure = exception == null;

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
            IsSecure = exception == null;

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

            if (!allowed.Contains(Scheme.ToLowerInvariant()))
                return new SecureUrlException("Protocol is insecure or unknown");

            // Note. No need to check .IsAbsoluteUri or .IsUnc as the SecureUrl constructor will consider
            //       these types of Uri badly formed
            return null;
        }
    }
}
