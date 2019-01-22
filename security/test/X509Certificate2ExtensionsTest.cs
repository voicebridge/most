using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using VoiceBridge.Most.Security.Extensions;
using Xunit;

namespace VoiceBridge.Most.Security.Test
{
    public class X509Certificate2ExtensionsTest
    {
        private readonly ECDsa elipticCurve = null;

        private readonly string SIGNING_CONTENT = "Hello World!";


        /// <summary>
        /// Constructor (testing set-up)
        /// </summary>
        public X509Certificate2ExtensionsTest()
        {
            elipticCurve = ECDsa.Create();
        }


        [Fact]
        public void VerifyChainPass()
        {
            Assert.True(Certificates.Valid.VerifyChain());
        }


        [Fact]
        public void VerifyChainFail()
        {
            Assert.False(Certificates.Invalid.VerifyChain());
        }


        [Fact]
        public void IsExpiredPass()
        {
            Assert.False(Certificates.Valid.IsExpired());
        }


        [Fact]
        public void IsExpiredFail()
        {
            Assert.True(MakeExpiredCert("expired").IsExpired());
            Assert.True(Certificates.Expired.IsExpired());
        }


        [Fact]
        public void IsRevokedPass()
        {
            Assert.False(Certificates.Valid.IsRevoked());
        }


        [Fact]
        public void IsRevokedFail()
        {
            // Expiry check
            Assert.True(MakeExpiredCert("expired").IsRevoked());

            // Revocation check (online)
            Assert.True(Certificates.Revoked.IsRevoked());
        }


        [Fact]
        public void IsNameValidPass()
        {
            Assert.True(Certificates.Valid.IsNameValid("echo-api.amazon.com"));
        }


        [Fact]
        public void IsNameValidFail()
        {
            Assert.False(Certificates.Valid.IsNameValid("fail"));
        }


        [Fact]
        public void VerifySignaturePass()
        {
            var signature = SignContent(SIGNING_CONTENT);
            Assert.True(Certificates.WithPrivateKey.VerifySignature(signature, SIGNING_CONTENT));
        }


        [Fact]
        public void VerifySignatureFail()
        {
            Assert.False(Certificates.WithPrivateKey.VerifySignature("fail", SIGNING_CONTENT));
        }


        /// <summary>
        /// Make a self-signed certificate for the sake of testing
        /// </summary>
        private X509Certificate2 MakeExpiredCert(string commonName)
        {
            var request = new CertificateRequest($"cn={commonName}", elipticCurve, HashAlgorithmName.SHA256);
            return request.CreateSelfSigned(DateTimeOffset.Now.AddYears(-2), DateTimeOffset.Now.AddYears(-1));
        }


        /// <summary>
        /// Sign the given content with a previously self-signed pfx
        /// </summary>
        /// <returns>A Base64 encoded signature</returns>
        private string SignContent(string content)
        {
            var data = Encoding.UTF8.GetBytes(content);

            using (var cert = Certificates.WithPrivateKey)
            {
                using (var rsa = cert.GetRSAPrivateKey())
                {
                    data = rsa.SignData(data, HashAlgorithmName.SHA1, RSASignaturePadding.Pkcs1);
                    return Convert.ToBase64String(data);
                }
            }
        }

    }
}
