using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

using VoiceBridge.Most.Security.Extensions;

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
                var chain = stream.LoadChain();

                //
                // When parsing a byte[] that contains multiple certificates on macOS, X509Certificate2
                // will throw an Exception (Interop+AppleCrypto+AppleCommonCryptoCryptographicException)
                //
                // Since the Windows and Ubuntu Interop only uses the first certificate in a chain
                // in such a scenario, we'll do the same here to ensure consistent behaviour on macOS
                //
                // See here: https://stackoverflow.com/questions/54579288/unable-to-load-a-pem-from-a-resource-file-on-macos
                //           https://github.com/dotnet/corefx/issues/35163
                //

                return chain[0];
            }
        }


        /// <summary>
        /// Get a GetX509Certificate2 using the named pfx file and password
        /// </summary>
        private static X509Certificate2 GetX509Certificate2(string name, string password)
        {
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(Prefix + name))
            {
                byte[] data = stream.LoadBytes();

                // Without this adjustment, we'll get a System.PlatformNotSupportedException
                // (This platform does not support loading with EphemeralKeySet. Remove the flag to allow keys to be temporarily created on disk.)
                if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    return new X509Certificate2(data, password);
                }

                return new X509Certificate2(data, password, X509KeyStorageFlags.EphemeralKeySet);
            }
        }

    }
}
