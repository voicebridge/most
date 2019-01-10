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
            var uri = SECURE_URI_1.ToSecureUri();
            var wss = SECURE_URI_2.ToSecureUri();

            Assert.True(uri is SecureUri);
            Assert.True(uri.IsSecure);
            Assert.Equal(SECURE_URI_1, uri.AbsoluteUri);

            Assert.True(wss is SecureUri);
            Assert.True(wss.IsSecure);
            Assert.Equal(SECURE_URI_2, wss.AbsoluteUri);

            var exception = Assert.Throws<SecureUriException>(() => { INSECURE_URI.ToSecureUri(); });
            Assert.Equal(EXCEPTION_MESSAGE_PROTOCOL, exception.Message);
        }


        [Fact]
        public void SecureUriTests()
        {
            var uri = new Uri(SECURE_URI_1).ToSecure();
            var wss = new Uri(SECURE_URI_2).ToSecure();

            Assert.True(uri is SecureUri);
            Assert.True(uri.IsSecure);
            Assert.Equal(SECURE_URI_1, uri.AbsoluteUri);

            Assert.True(wss is SecureUri);
            Assert.True(wss.IsSecure);
            Assert.Equal(SECURE_URI_2, wss.AbsoluteUri);

            var exception = Assert.Throws<SecureUriException>(() => { new Uri("http://www.insecure.com").ToSecure(); });
            Assert.Equal(EXCEPTION_MESSAGE_PROTOCOL, exception.Message);
        }
    }
}
