using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace VoiceBridge.Most.Security
{
    /// <summary>
    /// Certificate helper methods
    /// </summary>
    public class CertificateHelpers
    {
        private static HttpClient client = new HttpClient();


        /// <summary>
        /// Download an X509 Certificate from the given uri
        /// </summary>
        public static async Task<X509Certificate2> DownloadAsync(string uri)
        {
            var response = await client.GetAsync(uri);
            var bytes = await response.Content.ReadAsByteArrayAsync();
            return new X509Certificate2(bytes);
        }


        /// <summary>
        /// Verify the given certificate's chain
        /// </summary>
        public static bool VerifyChain(X509Certificate2 certificate)
        {
            X509Chain certificateChain = new X509Chain();
            certificateChain.ChainPolicy.RevocationMode = X509RevocationMode.NoCheck;
            certificateChain.ChainPolicy.RevocationFlag = X509RevocationFlag.EntireChain;
            return certificateChain.Build(certificate);
        }


        /// <summary>
        /// Verify if the given certificate is valid
        /// </summary>
        public static bool IsRevoked(X509Certificate2 certificate)
        {
            var result = DateTime.Now < certificate.NotAfter && DateTime.Now > certificate.NotBefore;

            if (result)
            {
                X509Chain chain = new X509Chain();
                chain.ChainPolicy.RevocationMode = X509RevocationMode.Online;
                chain.ChainPolicy.RevocationFlag = X509RevocationFlag.EntireChain;
                chain.ChainPolicy.UrlRetrievalTimeout = new TimeSpan(1000);
                chain.ChainPolicy.VerificationTime = DateTime.Now;
                return chain.Build(certificate);
            }

            return false;
        }


        /// <summary>
        /// Verify that the given certificate hasn't expired
        /// </summary>
        public static bool IsExpired(X509Certificate2 certificate)
        {
            return !(DateTime.Now < certificate.NotAfter && DateTime.Now > certificate.NotBefore);
        }


        /// <summary>
        /// Verify the given certificate's name
        /// </summary>
        public static bool IsNameValid(X509Certificate2 certificate, string name)
        {
            return certificate.GetNameInfo(X509NameType.SimpleName, false).Equals(name);
        }
    }
}
