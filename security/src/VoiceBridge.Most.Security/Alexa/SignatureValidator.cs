using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using VoiceBridge.Most.VoiceModel.Alexa;

namespace VoiceBridge.Most.Security.Alexa
{
    /// <summary>
    /// Verifies request signatures to ensure an Alexa payload came from Amazon
    /// </summary>
    public class SignatureValidator : IRequestValidator<SkillRequest>
    {
        // Private members
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
        public async Task VerifyAsync(HttpRequest http, SkillRequest payload)
        {
            if (http == null)
                throw new ArgumentNullException(nameof(http));

            if (!http.Headers.TryGetValue(SecurityConstants.SignatureRequestHeader, out var signature) || string.IsNullOrWhiteSpace(signature))
                throw new SecurityException("Missing signature in request header");

            if (!http.Headers.TryGetValue(SecurityConstants.SignatureCertificateHeader, out var url) || url.Count == 0 || string.IsNullOrWhiteSpace(url[0]))
                throw new SecurityException("Missing certificate in request header");

            // Attempt to fetch the certificate from the cache
            var cacheHit = cache.TryGet(url, out var certificate);

            // TODO: Consider IsRevoked instead
            if(cacheHit && CertificateHelpers.IsExpired(certificate))
            {
                if(!cache.TryEvict(url))
                    throw new CertificateException("Could not evict expired certificate from the cache");

                cacheHit = false;
            }

            // Was there a hit?
            if (!cacheHit)
            {
                if (!url[0].IsCertificateChainUrl(out var exception))
                    throw exception;

                certificate = await CertificateHelpers.DownloadAsync(url[0]);

                if (CertificateHelpers.IsExpired(certificate))
                    throw new SecurityException("Signing certificate has expired");

                if (!CertificateHelpers.IsNameValid(certificate, SecurityConstants.SignatureDomainName))
                    throw new SecurityException("Signing certificate chain did not come from Amazon");

                if (!CertificateHelpers.VerifyChain(certificate))
                    throw new SecurityException("Signing certificate chain is invalid");

                if(!cache.TryAdd(url, certificate))
                    throw new CertificateException("Unable to add signing certificate to cache");
            }

            // TODO: Check the signature!

            throw new NotImplementedException();
        }


    }
}
