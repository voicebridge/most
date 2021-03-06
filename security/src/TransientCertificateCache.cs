﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace VoiceBridge.Most.Security
{
    /// <summary>
    /// Implements a concurrent, in-memory (transient), X509Certificate cach
    /// </summary>
    public class TransientCertificateCache : ICertificateCache
    {
        private ConcurrentDictionary<string, X509Certificate2> store = new ConcurrentDictionary<string, X509Certificate2>();


        /// </inheritdoc>
        public bool ContainsKey(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException(nameof(key));

            return store.ContainsKey(key);
        }


        /// </inheritdoc>
        public bool TryAdd(string key, X509Certificate2 certificate)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException(nameof(key));

            if (certificate == null)
                throw new ArgumentNullException(nameof(certificate));
            
            return store.TryAdd(key, certificate);
        }


        /// </inheritdoc>
        public bool TryGet(string key, out X509Certificate2 certificate)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException(nameof(key));

            certificate = null;
            return store.TryGetValue(key, out certificate);
        }


        /// </inheritdoc>
        public bool TryEvict(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException(nameof(key));

            return store.TryRemove(key, out var certificate);
        }
    }
}
