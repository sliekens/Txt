using System;
using System.IO;
using System.Text;
using Txt;
using Xunit;

namespace Text
{
    public class StreamTextSourceTests
    {
        [Fact]
        public void Ctor_ThrowsOnNullStream()
        {
            Assert.Throws<ArgumentNullException>(() => new StreamTextSource(null, Encoding.UTF8));
        }

        [Fact]
        public void Ctor_ThrowsOnNullEncoding()
        {
            Assert.Throws<ArgumentNullException>(() => new StreamTextSource(new PushbackInputStream(Stream.Null), null));
        }

        [Fact]
        public void Ctor_AcceptsEmptyString()
        {
            new StringTextSource(string.Empty);
        }

        [Fact]
        public void Read_One_BuffersLazily()
        {
            // Ensure that StreamTextSource.Read() does not buffer more bytes than it needs
            var text = "abcd";
            var encoding = Encoding.UTF8;
            var bytes = encoding.GetBytes(text);
            using (var stream = new MemoryStream(bytes))
            using (var input = new PushbackInputStream(stream))
            using (var textSource = new StreamTextSource(input, encoding))
            {
                var a = (char)textSource.Read();
                Assert.Equal('a', a);
                Assert.Equal(1, input.Position);

                var b = (char)textSource.Read();
                Assert.Equal('b', b);
                Assert.Equal(2, input.Position);

                var c = (char)textSource.Read();
                Assert.Equal('c', c);
                Assert.Equal(3, input.Position);
            }
        }

        [Fact]
        public void Read_Many_BuffersLazily()
        {
            // Ensure that StreamTextSource.Read() does not buffer more bytes than it needs
            var text = "abcd";
            var encoding = Encoding.UTF8;
            var bytes = encoding.GetBytes(text);
            using (var stream = new MemoryStream(bytes))
            using (var input = new PushbackInputStream(stream))
            using (var textSource = new StreamTextSource(input, encoding))
            {
                // Arbitrarily chosen array size, but bigger than the input length
                char[] buffer = new char[32];
                int len = textSource.Read(buffer, 0, 3);

                Assert.Equal(3, len);
                Assert.Equal('a', buffer[0]);
                Assert.Equal('b', buffer[1]);
                Assert.Equal('c', buffer[2]);
                Assert.Equal(default(char), buffer[3]);
                Assert.Equal(3, input.Position);
            }
        }

        [Fact]
        public void Unread_One()
        {
            var text = "abcd";
            var encoding = Encoding.UTF8;
            var bytes = encoding.GetBytes(text);
            using (var stream = new MemoryStream(bytes))
            using (var input = new PushbackInputStream(stream))
            using (var textSource = new StreamTextSource(input, encoding))
            {
                var a = (char)textSource.Read();
                Assert.Equal('a', a);
                Assert.Equal(1, input.Position);

                textSource.Unread(a);
                Assert.Equal(0, input.Position);

                a = (char)textSource.Read();
                Assert.Equal('a', a);
                Assert.Equal(1, input.Position);
            }
        }


        [Fact]
        public void Unread_Many()
        {
            var text = "abcd";
            var encoding = Encoding.UTF8;
            var bytes = encoding.GetBytes(text);
            using (var stream = new MemoryStream(bytes))
            using (var input = new PushbackInputStream(stream))
            using (var textSource = new StreamTextSource(input, encoding))
            {
                // Arbitrarily chosen array size, but bigger than the input length
                char[] buffer = new char[32];
                int len = textSource.Read(buffer, 0, 3);

                Assert.Equal(3, len);
                Assert.Equal('a', buffer[0]);
                Assert.Equal('b', buffer[1]);
                Assert.Equal('c', buffer[2]);
                Assert.Equal(default(char), buffer[3]);
                Assert.Equal(3, input.Position);

                textSource.Unread(buffer, 0, 3);
                Assert.Equal(0, input.Position);

                len = textSource.Read(buffer, 0, 4);
                Assert.Equal(4, len);
                Assert.Equal('a', buffer[0]);
                Assert.Equal('b', buffer[1]);
                Assert.Equal('c', buffer[2]);
                Assert.Equal('d', buffer[3]);
                Assert.Equal(4, input.Position);
            }
        }
    }
}