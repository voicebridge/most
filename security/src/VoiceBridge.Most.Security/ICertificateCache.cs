using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace VoiceBridge.Most.Security
{
    /// <summary>
    /// Interface for the storage and retrieval of X509 certificates
    /// </summary>
    public interface ICertificateCache
    {
        /// <summary>
        /// Determines if the given key exists in this cache
        /// </summary>
        bool ContainsKey(string key);


        /// <summary>
        /// Attempt to get a X509Certificate using the given key
        /// </summary>
        bool TryGet(string key, out X509Certificate2 certificate);


        /// <summary>
        /// Attempt to store a X509Certificate using the given key
        /// </summary>
        bool TryAdd(string key, X509Certificate2 certificate);


        /// <summary>
        /// Attempt to evict a X509Certificate using the given key
        /// </summary>
        bool TryEvict(string key);
    }
}
