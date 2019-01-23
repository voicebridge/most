using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace VoiceBridge.Most.Security.Test
{
    /// <summary>
    /// Certificates to be used for testing
    /// </summary>
    /// <remarks>
    /// Source: https://www.amazontrust.com/repository/
    /// </remarks>
    public static class Certificates
    {
        private const string Prefix = "VoiceBridge.Most.Security.Test.TestFiles.";


        /// <summary>
        /// Source: https://s3.amazonaws.com/echo.api/echo-api-cert-6.pem
        /// </summary>
        public static X509Certificate2 Invalid => GetX509Certificate2("invalid.pem");


        /// <summary>
        /// Source: https://s3.amazonaws.com/echo.api/echo-api-cert-6-ats.pem
        /// </summary>
        public static X509Certificate2 Valid => GetX509Certificate2("valid.pem");


        /// <summary>
        /// Source: https://www.sk.ee/upload/files/EID-SK.pem.crt
        /// </summary>
        public static X509Certificate2 Revoked => GetX509Certificate2("revoked.pem");


        /// <summary>
        /// Source: https://www.sk.ee/upload/files/Juur-SK.pem.crt
        /// </summary>
        public static X509Certificate2 Expired => GetX509Certificate2("expired.pem");


        /// <summary>
        /// Source: https://stackoverflow.com/questions/14267010/how-to-create-self-signed-ssl-certificate-for-test-purposes
        /// </summary>
        public static X509Certificate2 WithPrivateKey => GetX509Certificate2("cert.pfx", "password");


        /// <summary>
        /// Helper method to load a .pem and return it as a X509Certificate2
        /// </summary>
        private static X509Certificate2 GetX509Certificate2(string name)
        {
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(Prefix + name))
            {
                byte[] data = new byte[stream.Length];
                stream.Read(data, 0, (int)stream.Length);
                return new X509Certificate2(data);
            }
        }


        /// <summary>
        /// Get a GetX509Certificate2 using the named pfx file and password
        /// </summary>
        private static X509Certificate2 GetX509Certificate2(string name, string password)
        {
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(Prefix + name))
            {
                byte[] data = new byte[stream.Length];
                stream.Read(data, 0, (int)stream.Length);
                return new X509Certificate2(data, password, X509KeyStorageFlags.EphemeralKeySet);
            }
        }
    }
}
