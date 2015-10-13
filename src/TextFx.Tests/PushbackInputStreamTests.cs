namespace TextFx.Tests
{
    using System;
    using System.IO;

    using Xunit;

    public class PushbackInputStreamTests
    {
        [Fact]
        public void Ctor_ThrowsOnNullReference()
        {
            Assert.Throws<ArgumentNullException>(
                () =>
                new PushbackInputStream(null));
        }

        [Fact]
        public void Ctor_AcceptsStreamNull()
        {
            new PushbackInputStream(Stream.Null);
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
        public void CanWrite_WhenCanSeek_ReturnsTrue()
        {
            using (var stub = new FakeStream { OnCanReadGet = () => true, OnCanSeekGet = () => false })
            using (var pushbackStream = new PushbackInputStream(stub))
            {
                Assert.True(pushbackStream.CanWrite);
            }
        }

        [Fact]
        public void Read_ReadsFromBufferFirst()
        {
            const int Position = 128;
            var underlyingStreamReadCalled = false;
            var mock = new FakeStream
            {
                OnCanReadGet = () => true,
                OnCanSeekGet = () => false,
                OnPositionGet = () => Position,
                OnRead = (_, __, ___) =>
                    {
                        underlyingStreamReadCalled = true;
                        return 0;
                    }
            };
            using (var pushbackStream = new PushbackInputStream(mock))
            {
                var pushbackBytes = new byte[] { 1, 2, 4, 8 };
                pushbackStream.Write(pushbackBytes, 0, pushbackBytes.Length);

                var result = new byte[pushbackBytes.Length];
                var bytesRead = pushbackStream.Read(result, 0, pushbackBytes.Length);

                Assert.False(underlyingStreamReadCalled);
                Assert.Equal(pushbackBytes.Length, bytesRead);
                Assert.Equal(pushbackBytes, result);
            }
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
            var stub = new FakeStream
            {
                OnCanReadGet = () => true,
                OnCanSeekGet = () => false
            };
            using (var pushbackStream = new PushbackInputStream(stub))
            {
                Assert.Throws<NotSupportedException>(() => pushbackStream.Position);
            }
        }
    }
}