using System;
using Txt;
using Xunit;

namespace Text
{
    public class StringTextSourceTests
    {
        [Fact]
        public void Ctor_ThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => new StringTextSource(null));
        }

        [Fact]
        public void Ctor_AcceptsEmptyString()
        {
            new StringTextSource(string.Empty);
        }

        [Fact]
        public void Read_One()
        {
            var text = "abcd";
            using (var textSource = new StringTextSource(text))
            {
                var a = (char)textSource.Read();
                Assert.Equal('a', a);

                var b = (char)textSource.Read();
                Assert.Equal('b', b);

                var c = (char)textSource.Read();
                Assert.Equal('c', c);
            }
        }

        [Fact]
        public void Read_Many()
        {
            var text = "abcd";
            using (var textSource = new StringTextSource(text))
            {
                // Arbitrarily chosen array size, but bigger than the input length
                char[] buffer = new char[32];
                int len = textSource.Read(buffer, 0, 3);
                Assert.Equal(3, len);
                Assert.Equal('a', buffer[0]);
                Assert.Equal('b', buffer[1]);
                Assert.Equal('c', buffer[2]);
                Assert.Equal(default(char), buffer[3]);
            }
        }

        [Fact]
        public void Unread_One()
        {
            var text = "abcd";
            using (var textSource = new StringTextSource(text))
            {
                var a = (char)textSource.Read();
                Assert.Equal('a', a);

                textSource.Unread(a);

                a = (char)textSource.Read();
                Assert.Equal('a', a);
            }
        }


        [Fact]
        public void Unread_Many()
        {
            var text = "abcd";
            using (var textSource = new StringTextSource(text))
            {
                // Arbitrarily chosen array size, but bigger than the input length
                char[] buffer = new char[32];
                int len = textSource.Read(buffer, 0, 3);

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
    }
}