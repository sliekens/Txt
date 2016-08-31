using System;
using System.Threading.Tasks;
using Xunit;

namespace Txt.Core
{
    public class StringTextSourceDisposedBehavior
    {
        private const string Sentence = "The quick brown fox jumps over the lazy dog";

        [Fact]
        public void WhenDisposeTwice_ExpectNothing()
        {
            // Given that the text source has been disposed
            // When Dispose is called
            // Then nothing should happen
            var sut = new StringTextSource(Sentence);
            sut.Dispose();
            sut.Dispose();
        }

        [Fact]
        public void WhenPeekAfterDispose_ExpectObjectDisposedException()
        {
            // Given that the text source has been disposed
            // When Peek is called
            // Then an ObjectDisposedException is thrown
            var sut = new StringTextSource(Sentence);
            sut.Dispose();
            int peek;
            Assert.Throws<ObjectDisposedException>(() => peek = sut.Peek());
        }

        [Fact]
        public void WhenRead_ExpectObjectDisposedException()
        {
            // Given that the text source has been disposed
            // When Read is called
            // Then an ObjectDisposedException is thrown
            var sut = new StringTextSource(Sentence);
            sut.Dispose();
            int len;
            Assert.Throws<ObjectDisposedException>(() => len = sut.Read());
        }

        [Fact]
        public async Task WhenReadAsync_ExpectObjectDisposedException()
        {
            // Given that the text source has been disposed
            // When ReadAsync is called
            // Then an ObjectDisposedException is thrown
            var sut = new StringTextSource(Sentence);
            sut.Dispose();
            var buffer = new char[Sentence.Length];
            int len;
            await
                Assert.ThrowsAsync<ObjectDisposedException>(
                    async () => len = await sut.ReadAsync(buffer, 0, buffer.Length));
        }

        [Fact]
        public void WhenReadBlock_ExpectObjectDisposedException()
        {
            // Given that the text source has been disposed
            // When ReadBlock is called
            // Then an ObjectDisposedException is thrown
            var sut = new StringTextSource(Sentence);
            sut.Dispose();
            int len;
            var buffer = new char[Sentence.Length];
            Assert.Throws<ObjectDisposedException>(() => len = sut.ReadBlock(buffer, 0, buffer.Length));
        }

        [Fact]
        public async Task WhenReadBlockAsync_ExpectObjectDisposedException()
        {
            // Given that the text source has been disposed
            // When ReadBlockAsync is called
            // Then an ObjectDisposedException is thrown
            var sut = new StringTextSource(Sentence);
            sut.Dispose();
            var buffer = new char[Sentence.Length];
            int len;
            await
                Assert.ThrowsAsync<ObjectDisposedException>(
                    async () => len = await sut.ReadBlockAsync(buffer, 0, buffer.Length));
        }

        [Fact]
        public void WhenReadWithBuffer_ExpectObjectDisposedException()
        {
            // Given that the text source has been disposed
            // When Read is called
            // Then an ObjectDisposedException is thrown
            var sut = new StringTextSource(Sentence);
            sut.Dispose();
            int len;
            var buffer = new char[Sentence.Length];
            Assert.Throws<ObjectDisposedException>(() => len = sut.Read(buffer, 0, buffer.Length));
        }

        [Fact]
        public void WhenUnread_ExpectObjectDisposedException()
        {
            // Given that the text source has been disposed
            // When Unread is called
            // Then an ObjectDisposedException is thrown
            var sut = new StringTextSource(Sentence);
            sut.Dispose();
            Assert.Throws<ObjectDisposedException>(() => sut.Unread('!'));
        }

        [Fact]
        public void WhenUnreadWithBuffer_ExpectObjectDisposedException()
        {
            // Given that the text source has been disposed
            // When Unread is called
            // Then an ObjectDisposedException is thrown
            var sut = new StringTextSource(Sentence);
            sut.Dispose();
            var buffer = new[] { '!', '!', '!' };
            Assert.Throws<ObjectDisposedException>(() => sut.Unread(buffer, 0, buffer.Length));
        }
    }
}
