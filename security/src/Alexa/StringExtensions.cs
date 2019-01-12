using System;
using System.Collections.Generic;
using System.Security;
using System.Text;
using VoiceBridge.Common;

namespace VoiceBridge.Most.Security.Alexa
{
    /// <summary>
    /// Extension methods that operate on strings
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Attempts to validate and form a valid Signing Certification Chain Url from the given text
        /// </summary>
        public static bool IsCertificateChainUrl(this string text, out Exception exception)
        {
            try
            {
                // Note: We don't need to normalize the Url as SecureUrl will do this for us
                var url = new SecureUrl(text);

                // Validate the protocol
                if (!url.Scheme.Equals("https", StringComparison.InvariantCulture))
                {
                    exception = new CertificateException("Signing certificate chain protocol is invalid");
                    return false;
                }

                // Validate the hostname
                if (!url.Host.Equals(SecurityConstants.SignatureCertificateHost, StringComparison.InvariantCulture))
                {
                    exception = new CertificateException("Signing certificate chain hostname is invalid");
                    return false;
                }

                // Validate the path prefix
                if (!url.AbsolutePath.StartsWith(SecurityConstants.SignatureCertificateUrlPrefix, StringComparison.InvariantCulture))
                {
                    exception = new CertificateException("Signing certificate chain path is invalid");
                    return false;
                }

                // Validate the port for HTTPS if it has been set explicitly
                if (!url.IsDefaultPort)
                {
                    exception = new CertificateException("Signing certificate chain port is invalid");
                    return false;
                }

                exception = null;
                return true;
            }
            catch (SecureUrlException)
            {
                exception = new CertificateException("Signing certificate chain Url is malformed");
                return false;
            }
        }
    }
}
