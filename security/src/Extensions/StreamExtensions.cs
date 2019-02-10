using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;

using VoiceBridge.Common.Extensions;

namespace VoiceBridge.Most.Security.Extensions
{
    /// <summary>
    /// Extensions to System.IO.Stream
    /// </summary>
    public static class StreamExtensions
    {
        // Byte marker for end of certificate
        private static byte[] CertificateMarker = new byte[] { 45, 45, 45, 45, 45, 69, 78, 68, 32, 67, 69, 82, 84, 73, 70, 73, 67, 65, 84, 69, 45, 45, 45, 45, 45 };


        /// <summary>
        /// Return the contents of this Stream as an array of bytes
        /// </summary>
        public static byte[] LoadBytes(this Stream stream)
        {
            byte[] data = new byte[stream.Length];
            var remaining = data.Length;
            var offset = 0;
            var read = 0;

            do
            {
                read = stream.Read(data, offset, remaining);
                offset += read;
                remaining -= read;
            }
            while (remaining > 0);
            return data;
        }


        /// <summary>
        /// Return the contents of this Stream as a chain of X509Certificates
        /// </summary>
        public static IList<X509Certificate2> LoadChain(this Stream stream)
        {
            var result = new List<X509Certificate2>();
            var data = stream.LoadBytes();
            var span = new Span<byte>(data);
            var certSpan = new Span<byte>();

            var index = 0;
            var lastIndex = 0;

            do
            {
                lastIndex = index;
                index = data.IndexOf(lastIndex, CertificateMarker);

                if (index == -1)
                    break;

                index += CertificateMarker.Length;
                certSpan = span.Slice(lastIndex, index - lastIndex);
                result.Add(new X509Certificate2(certSpan.ToArray()));
            }
            while (index > -1);

            if(result.Count == 0 && lastIndex == 0)
            {
                result.Add(new X509Certificate2(data));
            }

            return result.AsReadOnly();
        }
    }
}
