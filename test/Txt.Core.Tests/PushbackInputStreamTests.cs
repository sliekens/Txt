using System;
using System.IO;
using System.Threading;
using Xunit;

namespace Txt.Core
{
    public class PushbackInputStreamTests
    {
        [Fact]
        public void CanWrite_WhenCanSeek_ReturnsTrue()
        {
            var stub = new FakeStream { OnCanReadGet = () => true, OnCanSeekGet = () => false };

            var sut = new PushbackInputStream(stub);
            using (stub)
            using (sut)
            {
                Assert.True(sut.CanWrite);
            }
        }

        [Fact]
        public void CanWrite_WhenNotCanSeek_ReturnsFalse()
        {
            using (var stub = new FakeStream { OnCanReadGet = () => true, OnCanSeekGet = () => true })
            using (var pushbackStream = new PushbackInputStream(stub))
            {
                Assert.False(pushbackStream.CanWrite);
            }
        }

        [Fact]
        public void Ctor_AcceptsStreamNull()
        {
            new PushbackInputStream(Stream.Null);
        }

        [Fact]
        public void Ctor_ThrowsOnNullReference()
        {
            Assert.Throws<ArgumentNullException>(() => new PushbackInputStream(null));
        }

        [Fact]
        public void Position_WhenCanSeek_ReturnsPositionOfUnderlyingStream()
        {
            var stub = new FakeStream
                           {
                               OnCanReadGet = () => true,
                               OnCanSeekGet = () => true,
                               OnPositionGet = () => 128
                           };
            using (var pushbackStream = new PushbackInputStream(stub))
            {
                var position = pushbackStream.Position;
                Assert.Equal(128, position);
            }
        }

        [Fact]
        public void Position_WhenNotCanSeek_ThrowsNotSupportedException()
        {
            var stub = new FakeStream { OnCanReadGet = () => true, OnCanSeekGet = () => false };
            using (var pushbackStream = new PushbackInputStream(stub))
            {
                Assert.Throws<NotSupportedException>(() => pushbackStream.Position);
            }
        }

        [Fact]
        public void Read_ReadsFromBufferFirst()
        {
            const int Position = 128;
            var callsToRead = 0;
            var mock = new FakeStream
                           {
                               OnCanReadGet = () => true,
                               OnCanSeekGet = () => false,
                               OnPositionGet = () => Position,
                               OnReadByteArrayInt32Int32 = (_, __, ___) =>
                                   {
                                       callsToRead++;
                                       return 0;
                                   }
                           };
            using (var pushbackStream = new PushbackInputStream(mock))
            {
                var pushbackBytes = new byte[] { 1, 2, 4, 8 };
                pushbackStream.Write(pushbackBytes, 0, pushbackBytes.Length);

                // Read as many bytes as we wrote to the pusback buffer
                // The stream wrapper should not touch the underlying stream
                var result = new byte[pushbackBytes.Length];
                var bytesRead = pushbackStream.Read(result, 0, result.Length);

                Assert.Equal(0, callsToRead);
                Assert.Equal(pushbackBytes.Length, bytesRead);
                Assert.Equal(pushbackBytes, result);

                // The pushback buffer should be empty at this point;
                // a subsequent read should hit the underlying stream
                pushbackStream.Read(result, 0, result.Length);
                Assert.Equal(1, callsToRead);
            }
        }

        [Fact]
        public async void ReadAsync_ReadsFromBufferFirst()
        {
            const int Position = 128;
            var callsToRead = 0;

            // MEMO: all calls to PushbackInputStream.ReadAsync(...) are routed to PushbackInputStream.Read(...)
            // if we mocked FakeStream.ReadAsync(...), it would never be called
            // that is why the synchronous method is mocked instead
            var mock = new FakeStream
                           {
                               OnCanReadGet = () => true,
                               OnCanSeekGet = () => false,
                               OnPositionGet = () => Position,
                               OnReadByteArrayInt32Int32 = (_, __, ___) =>
                                   {
                                       callsToRead++;
                                       return 0;
                                   }
                           };
            using (var pushbackStream = new PushbackInputStream(mock))
            {
                var pushbackBytes = new byte[] { 1, 2, 4, 8 };
                pushbackStream.Write(pushbackBytes, 0, pushbackBytes.Length);

                // Read as many bytes as we wrote to the pusback buffer
                // The stream wrapper should not touch the underlying stream
                var result = new byte[pushbackBytes.Length];
                var bytesRead = await pushbackStream.ReadAsync(result, 0, result.Length);

                Assert.Equal(0, callsToRead);
                Assert.Equal(pushbackBytes.Length, bytesRead);
                Assert.Equal(pushbackBytes, result);

                // The pushback buffer should be empty at this point;
                // a subsequent read should hit the underlying stream
                await pushbackStream.ReadAsync(result, 0, result.Length, CancellationToken.None);
                Assert.Equal(1, callsToRead);
            }
        }

        [Fact]
        public void Write_DoesNotWriteToUnderlyingStream()
        {
            var callsToWrite = 0;
            var stub = new FakeStream
                           {
                               OnCanReadGet = () => true,
                               OnCanWriteGet = () => true,
                               OnCanSeekGet = () => false,
                               OnWriteByteArrayInt32Int32 = (_, __, ___) => callsToWrite++
                           };
            var sut = new PushbackInputStream(stub);
            using (sut)
            {
                var buffer = new byte[] { 0x8a, 0x8f, 0x1c, 0xdd, 0x7a, 0xa7, 0xd0, 0x99, 0xa3, 0x4b };
                sut.Write(buffer, 0, 10);
            }

            Assert.Equal(0, callsToWrite);
        }

        [Fact]
        public void Write_WhenCanSeek_ThrowsIOException()
        {
            var stub = new FakeStream
                           {
                               OnCanReadGet = () => true,
                               OnCanSeekGet = () => true,
                               OnPositionGet = () => 128
                           };

            var pushbackBytes = new byte[8];
            using (var pushbackStream = new PushbackInputStream(stub))
            {
                Assert.Throws<IOException>(() => pushbackStream.Write(pushbackBytes, 0, pushbackBytes.Length));
            }
        }

        [Fact]
        public async void WriteAsync_DoesNotWriteToUnderlyingStream()
        {
            var callsToWrite = 0;

            // MEMO: all calls to PushbackInputStream.WriteAsync(...) are routed to PushbackInputStream.Write(...)
            // if we mocked FakeStream.WriteAsync(...), it would never be called
            // that is why the synchronous method is mocked instead
            var stub = new FakeStream
                           {
                               OnCanReadGet = () => true,
                               OnCanWriteGet = () => true,
                               OnCanSeekGet = () => false,
                               OnWriteByteArrayInt32Int32 = (_, __, ___) => callsToWrite++
                           };
            var sut = new PushbackInputStream(stub);
            using (sut)
            {
                var buffer = new byte[] { 0x8a, 0x8f, 0x1c, 0xdd, 0x7a, 0xa7, 0xd0, 0x99, 0xa3, 0x4b };
                await sut.WriteAsync(buffer, 0, 10, CancellationToken.None);
            }

            Assert.Equal(0, callsToWrite);
        }
    }
}