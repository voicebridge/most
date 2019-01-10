using System;
using VoiceBridge.Common.Extensions;
using Xunit;

namespace VoiceBridge.Common.Test
{
    public class SecureUriTest
    {
        // This is to prevent the static setting tests in the Override() method
        // from interrupting the Pass() and Fail() tests if run in parallel
        private object _lock = new object();

        private const string SECURE_URI_1 = "https://www.secure.com/";
        private const string SECURE_URI_2 = "wss://socket.com/";

        private const string INSECURE_URI = "http://www.insecure.com/";
        private const string INSECURE_FTP_URI = "ftp://www.why.bother.com/";
        private const string INSECURE_REALTIVE_URI = "test/test.txt";
        private const string INSECURE_UNC_PATH = @"\\server\folder";
        private const string INSECURE_LOCAL_FILE = "file://c/test.txt";
        private const string INSECURE_LOOPBACK_ADDRESS = "https://127.0.0.1";

        private const string EXCEPTION_MESSAGE_BADLY_FORMED = "Uri is not well formed";
        private const string EXCEPTION_MESSAGE_LOCAL_FILE = "Uri is a local file";
        private const string EXCEPTION_INVALID_URI_FORMAT = "Invalid URI: The format of the URI could not be determined.";
        private const string EXCEPTION_MESSAGE_LOOPBACK_ADDRESS = "Uri is a loopback address";
        private const string EXCEPTION_MESSAGE_NOT_ABSOLUTE = "Uri is not absolute";
        private const string EXCEPTION_MESSAGE_PROTOCOL = "Protocol is insecure or unknown";


        /// <summary>
        /// Belts and braces on Test deconstruction
        /// </summary>
        ~SecureUriTest()
        {
            SecureUri.USE_UNSAFE_FOR_LOCAL_TESTING = false;
        }


        [Fact]
        public void Pass()
        {
            lock (_lock)
            {
                Assert.False(SecureUri.USE_UNSAFE_FOR_LOCAL_TESTING);

                var uri = new SecureUri(SECURE_URI_1);
                Assert.True(uri.IsSecure);
                Assert.Equal(SECURE_URI_1, uri.AbsoluteUri);

                uri = new SecureUri(SECURE_URI_2);
                Assert.True(uri.IsSecure);
                Assert.Equal(SECURE_URI_2, uri.AbsoluteUri);
            }
        }


        [Fact]
        public void Fail()
        {
            lock (_lock)
            {
                Assert.False(SecureUri.USE_UNSAFE_FOR_LOCAL_TESTING);

                var exception = Assert.Throws<SecureUriException>(() => { new SecureUri(INSECURE_URI); });
                Assert.Equal(EXCEPTION_MESSAGE_PROTOCOL, exception.Message);

                exception = Assert.Throws<SecureUriException>(() => { new SecureUri(INSECURE_FTP_URI); });
                Assert.Equal(EXCEPTION_MESSAGE_PROTOCOL, exception.Message);

                exception = Assert.Throws<SecureUriException>(() => { new SecureUri(INSECURE_UNC_PATH); });
                Assert.Equal(EXCEPTION_MESSAGE_BADLY_FORMED, exception.Message);

                exception = Assert.Throws<SecureUriException>(() => { new SecureUri(INSECURE_LOCAL_FILE); });
                Assert.Equal(EXCEPTION_MESSAGE_LOCAL_FILE, exception.Message);

                exception = Assert.Throws<SecureUriException>(() => { new SecureUri(INSECURE_LOOPBACK_ADDRESS); });
                Assert.Equal(EXCEPTION_MESSAGE_LOOPBACK_ADDRESS, exception.Message);

                var uriFormatException = Assert.Throws<UriFormatException>(() => { new SecureUri(INSECURE_REALTIVE_URI); });
                Assert.Equal(EXCEPTION_INVALID_URI_FORMAT, uriFormatException.Message);
            }
        }


        [Fact]
        public void Override()
        {
            lock (_lock)
            {
                Assert.False(SecureUri.USE_UNSAFE_FOR_LOCAL_TESTING);

                // You should NEVER do this in production code
                SecureUri.USE_UNSAFE_FOR_LOCAL_TESTING = true;
                Assert.True(SecureUri.USE_UNSAFE_FOR_LOCAL_TESTING);

                // Allow insecure Uris for the sake of testing
                var uri = new SecureUri(SECURE_URI_1);
                Assert.True(uri.IsSecure);

                uri = new SecureUri(SECURE_URI_2);
                Assert.True(uri.IsSecure);

                uri = new SecureUri(INSECURE_URI);
                Assert.False(uri.IsSecure);

                uri = new SecureUri(INSECURE_FTP_URI);
                Assert.False(uri.IsSecure);

                uri = new SecureUri(INSECURE_UNC_PATH);
                Assert.False(uri.IsSecure);

                uri = new SecureUri(INSECURE_LOCAL_FILE);
                Assert.False(uri.IsSecure);

                uri = new SecureUri(INSECURE_LOOPBACK_ADDRESS);
                Assert.False(uri.IsSecure);

                
                var uriFormatException = Assert.Throws<UriFormatException>(() => { new SecureUri(INSECURE_REALTIVE_URI); });
                Assert.Equal(EXCEPTION_INVALID_URI_FORMAT, uriFormatException.Message);

                // Put things back to how they should be
                SecureUri.USE_UNSAFE_FOR_LOCAL_TESTING = false;
                Assert.False(SecureUri.USE_UNSAFE_FOR_LOCAL_TESTING);

                // Sanity check
                var exception = Assert.Throws<SecureUriException>(() => { new SecureUri(INSECURE_URI); });
                Assert.Equal(EXCEPTION_MESSAGE_PROTOCOL, exception.Message);
            }
        }
    }
}
