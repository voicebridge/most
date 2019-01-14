using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
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
    }
}
