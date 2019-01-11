using System;
using VoiceBridge.Common.Extensions;
using Xunit;

namespace VoiceBridge.Common.Test
{
    public class ExtensionTest
    {
        private const string SECURE_URI_1 = "https://www.secure.com/";
        private const string SECURE_URI_2 = "wss://socket.com/";

        private const string INSECURE_URI = "http://www.insecure.com/";
        private const string EXCEPTION_MESSAGE_PROTOCOL = "Protocol is insecure or unknown";


        [Fact]
        public void StringTests()
        {
            var uri = SECURE_URI_1.ToSecureUrl();
            var wss = SECURE_URI_2.ToSecureUrl();

            Assert.True(uri is SecureUrl);
            Assert.True(uri.IsSecure);
            Assert.Equal(SECURE_URI_1, uri.AbsoluteUri);

            Assert.True(wss is SecureUrl);
            Assert.True(wss.IsSecure);
            Assert.Equal(SECURE_URI_2, wss.AbsoluteUri);

            var exception = Assert.Throws<SecureUrlException>(() => { INSECURE_URI.ToSecureUrl(); });
            Assert.Equal(EXCEPTION_MESSAGE_PROTOCOL, exception.Message);
        }


        [Fact]
        public void SecureUrlTests()
        {
            var uri = new Uri(SECURE_URI_1).ToSecure();
            var wss = new Uri(SECURE_URI_2).ToSecure();

            Assert.True(uri is SecureUrl);
            Assert.True(uri.IsSecure);
            Assert.Equal(SECURE_URI_1, uri.AbsoluteUri);

            Assert.True(wss is SecureUrl);
            Assert.True(wss.IsSecure);
            Assert.Equal(SECURE_URI_2, wss.AbsoluteUri);

//            var exception = Assert.Throws<SecureUrlException>(() => { new Uri(INSECURE_URI).ToSecure(); });
//            Assert.Equal(EXCEPTION_MESSAGE_PROTOCOL, exception.Message);
        }
    }
}
