using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using VoiceBridge.Most.VoiceModel.Alexa;

namespace VoiceBridge.Most.Security.Alexa
{
    /// <summary>
    /// Verifies request signatures to ensure an Alexa payload came from Amazon
    /// </summary>
    public class SignatureValidator : IRequestValidator<SkillRequest>
    {
        // Private member
        private ICertificateCache cache;


        /// <summary>
        /// Constructor
        /// </summary>
        internal SignatureValidator(ICertificateCache cache)
        {
            this.cache = cache;
        }


        /// <summary>
        /// Verify the request
        /// </summary>
        public bool Verify(HttpRequest http, SkillRequest payload, out Exception exception)
        {
            if (http == null)
            {
                exception = new ArgumentNullException(nameof(http));
                return false;
            }

            if (!http.Headers.TryGetValue(SecurityConstants.SignatureRequestHeader, out var signature) || string.IsNullOrWhiteSpace(signature))
            {
                exception = new SecurityException("Missing signature in request header");
                return false;
            }

            if (!http.Headers.TryGetValue(SecurityConstants.SignatureCertificateHeader, out var certChainUrl) || string.IsNullOrWhiteSpace(certChainUrl))
            {
                exception = new SecurityException("Missing certificate in request header");
                return false;
            }

            // Attempt to use a cached value
            if (!cache.TryGet(certChainUrl, out var certificate))
            {
                if (!certChainUrl.ToString().IsCertificateChainUrl(out exception))
                    return false;

                // TODO: Download cert and validate its chain
                throw new NotImplementedException();
            }

            // TODO: Is the certificate still valid (java equivalent of certificate.checkValidity());
            throw new NotImplementedException();

            //exception = null;
            //return false;
        }
    }
}
