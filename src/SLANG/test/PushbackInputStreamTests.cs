using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SLANG
{
    using System.IO;
    using System.Net;
    using System.Text;

    [TestClass]
    public class PushbackInputStreamTests
    {
        [TestMethod]
        public void CanSeekMemoryStream()
        {
            using (var memoryStream = new MemoryStream())
            using (var pushbackStream = new PushbackInputStream(memoryStream))
            {
                Assert.IsTrue(pushbackStream.CanSeek);
            }
        }

        [TestMethod]
        public void CanRead()
        {
            using (var memoryStream = new MemoryStream())
            using (var pushbackStream = new PushbackInputStream(memoryStream))
            {
                Assert.IsTrue(pushbackStream.CanRead);
            }
        }

        [TestMethod]
        public void CannotWrite()
        {
            using (var memoryStream = new MemoryStream())
            using (var pushbackStream = new PushbackInputStream(memoryStream))
            {
                Assert.IsFalse(pushbackStream.CanWrite);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(IOException))]
        public void CannotPushbackToSeekableStreams()
        {
            using (var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes("Lorem Ipsum")))
            using (var pushbackStream = new PushbackInputStream(memoryStream))
            {
                byte[] buffer = new byte[4096];
                int result = pushbackStream.Read(buffer, 0, buffer.Length);
                pushbackStream.Unread(buffer, 0, result);
            }
        }

        [TestMethod]
        public void CanPushbackToNonSeekableStreams()
        {
            // TODO: write a test that does not rely on external systems
            var webRequest = WebRequest.CreateHttp("http://httpbin.org/stream-bytes/1024");
            using (var webResponse = webRequest.GetResponse())
            using (var responseStream = webResponse.GetResponseStream())
            using (var pushbackStream = new PushbackInputStream(responseStream))
            {
                byte[] buffer = new byte[4096];
                int result = pushbackStream.Read(buffer, 0, 512);
                Assert.AreEqual(512, result);
                pushbackStream.Unread(buffer, 0, 512);
                byte[] buffer2 = new byte[4096];
                int result2 = pushbackStream.Read(buffer2, 0, 1024);
                int result3 = pushbackStream.Read(buffer2, result2, 1024);
                Assert.AreEqual(1024, result2 + result3);
            }
        }
    }
}
