using System;
using System.Linq;
using Xunit;

namespace Txt.Core
{
    public class StringTextSourceTests
    {
        private const string EmptyString = "";

        private const string Sentence = "The quick brown fox jumps over the lazy dog";

        [Fact]
        public void WhenRead_ExpectCharacterIsReturnedAndConsumed()
        {
            // Given that there is text to be read
            // When Read is called
            // Then the next available character is returned
            //  And the character is consumed
            using (var sut = new StringTextSource("abc"))
            {
                var read = (char)sut.Read();
                Assert.Equal('a', read);

                // Verify behavior by peeking at the next character
                var peek = (char)sut.Peek();
                Assert.NotEqual('a', peek);
            }
        }

        [Fact]
        public void WhenUnread_ExpectCharacterIsPushedBack()
        {
            // Given that there is text to be read
            // When Unread is called
            // Then the next available character is the one that was pushed back
            using (var sut = new StringTextSource("abc"))
            {
                sut.Unread('!');

                // Verify behavior by peeking at the next character
                var peek = (char)sut.Peek();
                Assert.Equal('!', peek);
            }
        }

        [Fact]
        public void WhenReadWithBuffer_ExpectTextIsReturnedAndConsumed()
        {
            // Given that there is text to be read
            // When Read is called
            // Then the buffer contains the text
            //  And the text is consumed
            using (var sut = new StringTextSource(Sentence))
            {
                var buffer = new char[Sentence.Length];
                var len = sut.Read(buffer, 0, buffer.Length);
                Assert.Equal(buffer.Length, len);
                Assert.Equal(Sentence.ToCharArray(), buffer);

                // Verify behavior by peeking at the next character
                Assert.Equal(-1, sut.Peek());
            }
        }

        [Fact]
        public void WhenReadWithBufferBiggerThanTextSource_ExpectResultLessThanBufferSize()
        {
            // Given that there is text to be read
            // When Read is called with a buffer
            //  And the buffer length is larger than the length of the text
            // Then the buffer contains the text
            //  And the returned length is less than the length of the buffer
            //  And the text is consumed
            using (var sut = new StringTextSource(Sentence))
            {
                var buffer = new char[Sentence.Length * 2];
                var len = sut.Read(buffer, 0, buffer.Length);
                Assert.Equal(Sentence.Length, len);
                Assert.Equal(Sentence.ToCharArray(), buffer.Take(len));

                // Verify behavior by peeking at the next character
                Assert.Equal(-1, sut.Peek());
            }
        }

        [Fact]
        public void Unread_Many()
        {
            var text = "abcd";
            using (var textSource = new StringTextSource(text))
            {
                // Arbitrarily chosen array size, but bigger than the input length
                var buffer = new char[32];
                var len = textSource.Read(buffer, 0, 3);
                Assert.Equal(3, len);
                Assert.Equal('a', buffer[0]);
                Assert.Equal('b', buffer[1]);
                Assert.Equal('c', buffer[2]);
                Assert.Equal(default(char), buffer[3]);
                textSource.Unread(buffer, 0, 3);
                len = textSource.Read(buffer, 0, 4);
                Assert.Equal(4, len);
                Assert.Equal('a', buffer[0]);
                Assert.Equal('b', buffer[1]);
                Assert.Equal('c', buffer[2]);
                Assert.Equal('d', buffer[3]);
            }
        }

        [Fact]
        public void WhenNewWithEmptyString_ExpectObjectIsCreated()
        {
            // When creating a new StringTextSource with an empty string
            // Then no exception is thrown
            var sut = new StringTextSource(EmptyString);
        }

        [Fact]
        public void WhenNewWithNullArgument_ExpectArgumentNullException()
        {
            // When creating a new StringTextSource with a null argument
            // Then an ArgumentNullException is thrown
            StringTextSource sut;
            Assert.Throws<ArgumentNullException>(() => sut = new StringTextSource(null));
        }

        [Fact]
        public void WhenPeekingEndOfSource_ExpectMinusOne()
        {
            // Given that the text source has been initialized with an string
            // When Peek is called
            // Then -1 is returned
            var sut = new StringTextSource(EmptyString);
            var peek = sut.Peek();
            Assert.Equal(-1, peek);
        }

        [Fact]
        public void WhenReadingEndOfSource_ExpectMinusOne()
        {
            // Given that the text source has been initialized with an string
            // When Read is called
            // Then -1 is returned
            var sut = new StringTextSource(EmptyString);
            var read = sut.Read();
            Assert.Equal(-1, read);
        }

        [Fact]
        public void WhenReadWithCountBeyondBufferLength_ExpectArgumentOutOfRangeException()
        {
            // Given that the text source has been initialized with a non-empty string
            // When Read is called with a count that exceeds the buffer length
            // Then an ArgumentOutOfRangeException is thrown
            var sut = new StringTextSource(Sentence);
            var buffer = new char[Sentence.Length];
            int read;
            Assert.Throws<ArgumentOutOfRangeException>(() => read = sut.Read(buffer, 0, buffer.Length + 1));
        }

        [Fact]
        public void WhenReadWithEmptyBuffer_ExpectArgumentNullException()
        {
            // Given that the text source has been initialized with a non-empty string
            // When Read is called with a buffer that is a null reference
            // Then an ArgumentNullException is thrown
            var sut = new StringTextSource(Sentence);
            int read;
            Assert.Throws<ArgumentNullException>(() => read = sut.Read(null, 0, Sentence.Length));
        }

        [Fact]
        public void WhenReadWithNegativeCount_ExpectArgumentOutOfRangeException()
        {
            // Given that the text source has been initialized with a non-empty string
            // When Read is called with a negative count
            // Then an ArgumentOutOfRangeException is thrown
            var sut = new StringTextSource(Sentence);
            var buffer = new char[Sentence.Length];
            int read;
            Assert.Throws<ArgumentOutOfRangeException>(() => read = sut.Read(buffer, 0, -1));
        }

        [Fact]
        public void WhenReadWithNegativeOffset_ExpectArgumentOutOfRangeException()
        {
            // Given that the text source has been initialized with a non-empty string
            // When Read is called with a negative offset
            // Then an ArgumentOutOfRangeException is thrown
            var sut = new StringTextSource(Sentence);
            var buffer = new char[Sentence.Length];
            int read;
            Assert.Throws<ArgumentOutOfRangeException>(() => read = sut.Read(buffer, -1, buffer.Length));
        }

        [Fact]
        public void WhenReadWithOffsetAndCountCombinationBeyondBufferLength_ExpectArgumentOutOfRangeException()
        {
            // Given that the text source has been initialized with a non-empty string
            // When Read is called with an offset and count whose sum exceeds the buffer length
            // Then an ArgumentOutOfRangeException is thrown
            var sut = new StringTextSource(Sentence);
            var buffer = new char[Sentence.Length];
            int read;
            Assert.Throws<ArgumentOutOfRangeException>(() => read = sut.Read(buffer, 1, buffer.Length));
        }

        [Fact]
        public void WhenReadWithOffsetBeyondBufferLength_ExpectArgumentOutOfRangeException()
        {
            // Given that the text source has been initialized with a non-empty string
            // When Read is called with an offset that is beyond the buffer length
            // Then an ArgumentOutOfRangeException is thrown
            var sut = new StringTextSource(Sentence);
            var buffer = new char[Sentence.Length];
            int read;
            Assert.Throws<ArgumentOutOfRangeException>(() => read = sut.Read(buffer, buffer.Length + 1, 0));
        }
    }
}
