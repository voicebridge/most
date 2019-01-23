using VoiceBridge.Most.Security.Extensions;
using Xunit;

namespace VoiceBridge.Most.Security.Test
{
    public class StringExtensionsTest
    {
        public const string SIGNING_CERT_CHAIN_URL_MALFORMED = "Signing certificate chain Url is malformed";
        public const string SIGNING_CERT_CHAIN_PROTOCOL_INVALID = "Signing certificate chain protocol is invalid";
        public const string SIGNING_CERT_CHAIN_HOSTNAME_INVALID = "Signing certificate chain hostname is invalid";
        public const string SIGNING_CERT_CHAIN_PATH_INVALID = "Signing certificate chain path is invalid";
        public const string SIGNING_CERT_CHAIN_PORT_INVALID = "Signing certificate chain port is invalid";


        /// <summary>
        /// Positive test cases from Amazon: https://developer.amazon.com/docs/custom-skills/host-a-custom-skill-as-a-web-service.html
        /// </summary>
        [Fact]
        public void AlexaCertificateChainUrlPass()
        {
            Assert.True("https://s3.amazonaws.com/echo.api/echo-api-cert.pem".IsAlexaCertificateChainUrl(out var exception));
            Assert.Null(exception);

            Assert.True("https://s3.amazonaws.com:443/echo.api/echo-api-cert.pem".IsAlexaCertificateChainUrl(out exception));
            Assert.Null(exception);

            Assert.True("https://s3.amazonaws.com/echo.api/../echo.api/echo-api-cert.pem".IsAlexaCertificateChainUrl(out exception));
            Assert.Null(exception);
        }


        /// <summary>
        /// Negative test cases from Amazon: https://developer.amazon.com/docs/custom-skills/host-a-custom-skill-as-a-web-service.html
        /// </summary>
        [Fact]
        public void AlexaCertificateChainUrlFail()
        {
            Assert.False("http://s3.amazonaws.com/echo.api/echo-api-cert.pem".IsAlexaCertificateChainUrl(out var exception));
            Assert.IsType<CertificateException>(exception);
            Assert.Equal(SIGNING_CERT_CHAIN_URL_MALFORMED, exception.Message);

            Assert.False("https://notamazon.com/echo.api/echo-api-cert.pem".IsAlexaCertificateChainUrl(out exception));
            Assert.IsType<CertificateException>(exception);
            Assert.Equal(SIGNING_CERT_CHAIN_HOSTNAME_INVALID, exception.Message);

            Assert.False("https://s3.amazonaws.com/EcHo.aPi/echo-api-cert.pem".IsAlexaCertificateChainUrl(out exception));
            Assert.IsType<CertificateException>(exception);
            Assert.Equal(SIGNING_CERT_CHAIN_PATH_INVALID, exception.Message);

            Assert.False("https://s3.amazonaws.com/invalid.path/echo-api-cert.pem".IsAlexaCertificateChainUrl(out exception));
            Assert.IsType<CertificateException>(exception);
            Assert.Equal(SIGNING_CERT_CHAIN_PATH_INVALID, exception.Message);

            Assert.False("https://s3.amazonaws.com:563/echo.api/echo-api-cert.pem".IsAlexaCertificateChainUrl(out exception));
            Assert.IsType<CertificateException>(exception);
            Assert.Equal(SIGNING_CERT_CHAIN_PORT_INVALID, exception.Message);
        }


        [Fact]
        public void AlexaCertificateChainUrlFailMisc()
        {
            Assert.False("http://www.insecure.com".IsAlexaCertificateChainUrl(out var exception));
            Assert.IsType<CertificateException>(exception);
            Assert.Equal(SIGNING_CERT_CHAIN_URL_MALFORMED, exception.Message);

            Assert.False("wss://www.insecure.com".IsAlexaCertificateChainUrl(out exception));
            Assert.IsType<CertificateException>(exception);
            Assert.Equal(SIGNING_CERT_CHAIN_PROTOCOL_INVALID, exception.Message);

            Assert.False("https://www.some.domain.com".IsAlexaCertificateChainUrl(out exception));
            Assert.IsType<CertificateException>(exception);
            Assert.Equal(SIGNING_CERT_CHAIN_HOSTNAME_INVALID, exception.Message);
        }
    }
}
