namespace TextFx.Tests
{
    using System.IO;

    using Xunit;

    public class PushbackInputStreamTests
    {
        [Fact]
        public void CanWrite_ReturnsFalseIfSeekingIsSupported()
        {
            using (var stub = new FakeStream { OnCanReadGet = () => true, OnCanSeekGet = () => true })
            using (var pushbackStream = new PushbackInputStream(stub))
            {
                Assert.False(pushbackStream.CanWrite);
            }
        }

        [Fact]
        public void CanWrite_ReturnsTrueIfSeekingIsNotSupported()
        {
            using (var stub = new FakeStream { OnCanReadGet = () => true, OnCanSeekGet = () => false })
            using (var pushbackStream = new PushbackInputStream(stub))
            {
                Assert.True(pushbackStream.CanWrite);
            }
        }

        [Fact]
        public void Ctor_SetsInitialPositionSameAsUnderlyingStream()
        {
            const int Position = 123;
            using (var stub = new FakeStream { OnCanReadGet = () => true, OnPositionGet = () => Position })
            using (var pushbackStream = new PushbackInputStream(stub))
            {
                Assert.Equal(Position, pushbackStream.Position);
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
        public void Write_SeekableStreamThrowsInvalidOperationException()
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
    }
}