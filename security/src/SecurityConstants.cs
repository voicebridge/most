using System;
using System.Collections.Generic;
using System.Text;

namespace VoiceBridge.Most.Security
{
    public class SecurityConstants
    {
        public class Alexa
        {
            public const string SignatureCertificateHost = "s3.amazonaws.com";
            public const string SignatureCertificateUrlPrefix = "/echo.api/";

            public const string SignatureRequestHeader = "Signature";
            public const string SignatureCertificateHeader = "SignatureCertChainUrl";
            public const string SignatureDomainName = "echo-api.amazon.com";
        }

        public class Platform
        {
            public const string Alexa = "Alexa";
            public const string GoogleHome = "GoogleHome";
        }
    }
}
