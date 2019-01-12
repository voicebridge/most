using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Xunit;

namespace VoiceBridge.Most.Security.Test
{
    public class CacheTest
    {
        public const string KEY_ONE = "key 1";
        public const string KEY_TWO = "key 2";
        public const string KEY_NONE = "none";

        private ECDsa elipticCurve = null;


        /// <summary>
        /// Constructor (testing set-up)
        /// </summary>
        public CacheTest()
        {
            elipticCurve = ECDsa.Create();
        }


        [Fact]
        public void AddTests()
        {
            ICertificateCache cache = new TransientCertificateCache();
            Assert.False(cache.ContainsKey(KEY_ONE));
            Assert.False(cache.ContainsKey(KEY_TWO));

            Assert.Throws<ArgumentException>(() => { cache.ContainsKey(null); });
            Assert.Throws<ArgumentNullException>(() => { cache.TryAdd(KEY_ONE, null);  });

            // Make some certs
            var cert = MakeSelfSignedCert("foobar");

            // Add cert to the cache
            Assert.True(cache.TryAdd(KEY_ONE, cert));
            Assert.True(cache.ContainsKey(KEY_ONE));
            Assert.False(cache.ContainsKey(KEY_NONE));

            // Overwrite the cache with the same item and key
            Assert.True(cache.TryAdd(KEY_ONE, cert));
            Assert.True(cache.ContainsKey(KEY_ONE));

            Assert.False(cache.ContainsKey(KEY_TWO));
        }


        [Fact]
        public void FetchTests()
        {
            ICertificateCache cache = new TransientCertificateCache();
            Assert.False(cache.ContainsKey(KEY_ONE));
            Assert.False(cache.ContainsKey(KEY_TWO));

            var one = MakeSelfSignedCert("one");
            var two = MakeSelfSignedCert("two");
            var three = MakeSelfSignedCert("three");

            Assert.True(cache.TryAdd(KEY_ONE, one));
            Assert.True(cache.TryAdd(KEY_TWO, two));

            Assert.True(cache.ContainsKey(KEY_ONE));
            Assert.True(cache.ContainsKey(KEY_TWO));
            Assert.False(cache.ContainsKey(KEY_NONE));

            Assert.True(cache.TryGet(KEY_TWO, out var cert));
            Assert.Equal(two, cert);

            Assert.True(cache.TryAdd(KEY_ONE, three));
            Assert.True(cache.ContainsKey(KEY_ONE));

            Assert.True(cache.TryGet(KEY_ONE, out cert));
            Assert.Equal(three, cert);

            Assert.False(cache.TryGet(KEY_NONE, out cert));
            Assert.Null(cert);
        }


        [Fact]
        public void EvictTests()
        {
            ICertificateCache cache = new TransientCertificateCache();
            Assert.False(cache.ContainsKey(KEY_ONE));
            Assert.False(cache.ContainsKey(KEY_TWO));

            var one = MakeSelfSignedCert("foo");
            var two = MakeSelfSignedCert("bar");

            Assert.True(cache.TryAdd(KEY_ONE, one));
            Assert.True(cache.TryAdd(KEY_TWO, two));

            Assert.True(cache.ContainsKey(KEY_ONE));
            Assert.True(cache.ContainsKey(KEY_TWO));
            Assert.False(cache.ContainsKey(KEY_NONE));

            Assert.True(cache.TryEvict(KEY_ONE));

            Assert.False(cache.ContainsKey(KEY_ONE));
            Assert.True(cache.ContainsKey(KEY_TWO));
            Assert.False(cache.ContainsKey(KEY_NONE));
        }


        /// <summary>
        /// Make a self-signed certificate for the sake of testing
        /// </summary>
        private X509Certificate2 MakeSelfSignedCert(string commonName)
        {
            var request = new CertificateRequest($"cn={commonName}", elipticCurve, HashAlgorithmName.SHA256);
            return request.CreateSelfSigned(DateTimeOffset.Now, DateTimeOffset.Now.AddYears(5));
        }
    }
}
