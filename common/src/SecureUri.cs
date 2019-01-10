using System;
using System.Collections.Generic;
using System.Text;

namespace VoiceBridge.Common
{
    /// <summary>
    /// An immutable, sealed, Uri class that enforces a secure scheme
    /// </summary>
    public sealed class SecureUri : Uri
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
        /// Construct a Secure Uri using the given string
        /// </summary>
        public SecureUri(string uri)
            : base(uri)
        {
            var exception = Validate();
            IsSecure = exception == null;

            if (!USE_UNSAFE_FOR_LOCAL_TESTING && exception != null)
                throw exception;
        }


        /// <summary>
        /// Construct a Secure Uri using the given Uri
        /// </summary>
        public SecureUri(Uri uri)
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
        private SecureUriException Validate()
        {
            if (!IsWellFormedOriginalString())
                return new SecureUriException("Uri is not well formed");

            if(IsFile)
                return new SecureUriException("Uri is a local file");

            if(IsLoopback)
                return new SecureUriException("Uri is a loopback address");

            if (!allowed.Contains(Scheme.ToLowerInvariant()))
                return new SecureUriException("Protocol is insecure or unknown");

            // Note. No need to check .IsAbsoluteUri or .IsUnc as the SecureUri constructor will consider
            //       these types of Uri badly formed
            return null;
        }
    }
}
