using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace VoiceBridge.Most.Security.Extensions
{
    /// <summary>
    /// X509Certificate2 extensions methods
    /// </summary>
    public static class X509Certificate2Extensions
    {
        /// <summary>
        /// Verify this certificate's chain
        /// </summary>
        public static bool VerifyChain(this X509Certificate2 certificate)
        {
            X509Chain certificateChain = new X509Chain();
            certificateChain.ChainPolicy.RevocationMode = X509RevocationMode.NoCheck;
            certificateChain.ChainPolicy.RevocationFlag = X509RevocationFlag.EntireChain;
            return certificateChain.Build(certificate);
        }


        /// <summary>
        /// Verify this certificate is valid
        /// </summary>
        public static bool IsRevoked(this X509Certificate2 certificate)
        {
            var result = DateTime.Now < certificate.NotAfter && DateTime.Now > certificate.NotBefore;

            if (result)
            {
                X509Chain chain = new X509Chain();
                chain.ChainPolicy.RevocationMode = X509RevocationMode.Online;

                //
                // Since we are only loading one certificate from the chain, it is not
                // appropriate to do this. It will actually fail on Ubuntu (Windows and macOS is fine)
                //
                // chain.ChainPolicy.RevocationFlag = X509RevocationFlag.EntireChain;
                //
                chain.ChainPolicy.UrlRetrievalTimeout = new TimeSpan(1000);
                chain.ChainPolicy.VerificationTime = DateTime.Now;
                return !chain.Build(certificate);
            }

            return true;
        }


        /// <summary>
        /// Verify this certificate hasn't expired
        /// </summary>
        public static bool IsExpired(this X509Certificate2 certificate)
        {
            return !(DateTime.Now < certificate.NotAfter && DateTime.Now > certificate.NotBefore);
        }


        /// <summary>
        /// Verify this certificate's simple name
        /// </summary>
        public static bool IsNameValid(this X509Certificate2 certificate, string name)
        {
            return certificate.GetNameInfo(X509NameType.SimpleName, false).Equals(name);
        }


        /// <summary>
        /// Verify the given payload's signature using this certificate
        /// </summary>
        public static bool VerifySignature(this X509Certificate2 certificate, string signature, string content)
        {
            var sig = Convert.FromBase64String(signature);
            var rsa = certificate.GetRSAPublicKey();
            return rsa.VerifyData(Encoding.UTF8.GetBytes(content), sig, HashAlgorithmName.SHA1, RSASignaturePadding.Pkcs1);
        }
    }
}
