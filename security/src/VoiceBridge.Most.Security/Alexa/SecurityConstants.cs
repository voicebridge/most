using System;
using System.Collections.Generic;
using System.Text;

namespace VoiceBridge.Most.Security.Alexa
{
    /// <summary>
    /// Constants to be used when securint Alexa
    /// </summary>
    public class SecurityConstants
    {
        public const string SignatureCertificateHost = "s3.amazonaws.com";
        public const string SignatureCertificateUrlPrefix = "/echo.api/";

        public const string SignatureRequestHeader = "Signature";
        public const string SignatureCertificateHeader = "SignatureCertChainUrl";
        public const string SignatureDomainName = "echo-api.amazon.com";
    }
}
