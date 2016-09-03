using System;
using System.Text;
using Xunit;

namespace Txt.Core
{
    public partial class TextSourceTests
    {
        public class Ctors
        {
            [Fact]
            public void CtorAcceptsEmptyData()
            {
                Assert.NotNull(new TextSourceCtors(new char[0]));
            }

            [Fact]
            public void CtorAcceptsEmptyData2()
            {
                Assert.NotNull(new TextSourceCtors(new char[0], 0));
            }

            [Fact]
            public void CtorAcceptsEmptyData3()
            {
                Assert.NotNull(new TextSourceCtors(new char[0], 0, 0));
            }

            [Theory]
            [InlineData("", 0)]
            [InlineData("012345", 0)]
            [InlineData("012345", 1)]
            [InlineData("012345", 2)]
            [InlineData("012345", 3)]
            [InlineData("012345", 4)]
            [InlineData("012345", 5)]
            [InlineData("012345", 6)]
            public void CtorAcceptsIndexInRange(string data, int startIndex)
            {
                Assert.NotNull(new TextSourceCtors(data.ToCharArray(), startIndex));
            }

            [Theory]
            [InlineData("", 0, 0)]
            [InlineData("012345", 0, 0)]
            [InlineData("012345", 1, 0)]
            [InlineData("012345", 2, 0)]
            [InlineData("012345", 3, 0)]
            [InlineData("012345", 4, 0)]
            [InlineData("012345", 5, 0)]
            [InlineData("012345", 6, 0)]
            public void CtorAcceptsIndexInRange(string data, int startIndex, int length)
            {
                Assert.NotNull(new TextSourceCtors(data.ToCharArray(), startIndex, length));
            }

            [Theory]
            [InlineData("", 0, 0)]
            [InlineData("1", 0, 1)]
            [InlineData("12", 0, 2)]
            [InlineData("123", 0, 3)]
            [InlineData("1234", 0, 4)]
            [InlineData("12345", 0, 5)]
            [InlineData("12345", 0, 4)]
            [InlineData("12345", 0, 3)]
            [InlineData("12345", 0, 2)]
            [InlineData("12345", 0, 1)]
            [InlineData("12345", 0, 0)]
            public void CtorAcceptsLengthInRange(string data, int startIndex, int length)
            {
                Assert.NotNull(new TextSourceCtors(data.ToCharArray(), startIndex, length));
            }

            [Fact]
            public void CtorAcceptsNonEmptyData()
            {
                var data = "Hello World!".ToCharArray();
                Assert.NotNull(new TextSourceCtors(data));
            }

            [Fact]
            public void CtorAcceptsNonEmptyData2()
            {
                var data = "Hello World!".ToCharArray();
                Assert.NotNull(new TextSourceCtors(data, 0));
            }

            [Fact]
            public void CtorAcceptsNonEmptyData3()
            {
                var data = "Hello World!".ToCharArray();
                Assert.NotNull(new TextSourceCtors(data, 0, 0));
            }

            [Theory]
            [InlineData("", 0, -1)]
            [InlineData("", 0, -10000)]
            [InlineData("", 0, int.MinValue)]
            [InlineData("", 0, 1)]
            [InlineData("", 0, 10000)]
            [InlineData("", 0, int.MaxValue)]
            [InlineData("012345", 0, -1)]
            [InlineData("012345", 0, int.MinValue)]
            [InlineData("012345", 0, 7)]
            [InlineData("012345", 0, int.MaxValue)]
            public void CtorRejectsLengthOutOfRange(string data, int startIndex, int length)
            {
                Assert.Throws<ArgumentOutOfRangeException>(
                    "length",
                    () => new TextSourceCtors(data.ToCharArray(), startIndex, length));
            }

            [Fact]
            public void CtorRejectsNull()
            {
                Assert.Throws<ArgumentNullException>("data", () => new TextSourceCtors(null));
            }

            [Fact]
            public void CtorRejectsNull2()
            {
                Assert.Throws<ArgumentNullException>("data", () => new TextSourceCtors(null, 0));
            }

            [Fact]
            public void CtorRejectsNull3()
            {
                Assert.Throws<ArgumentNullException>("data", () => new TextSourceCtors(null, 0, 0));
            }

            [Theory]
            [InlineData("", -1)]
            [InlineData("", -10000)]
            [InlineData("", int.MinValue)]
            [InlineData("", 1)]
            [InlineData("", 10000)]
            [InlineData("", int.MaxValue)]
            [InlineData("012345", -1)]
            [InlineData("012345", int.MinValue)]
            [InlineData("012345", 7)]
            [InlineData("012345", int.MaxValue)]
            public void CtorRejectsStartIndexOutOfRange(string data, int startIndex)
            {
                Assert.Throws<ArgumentOutOfRangeException>(
                    "startIndex",
                    () => new TextSourceCtors(data.ToCharArray(), startIndex));
            }

            [Theory]
            [InlineData("", -1, 0)]
            [InlineData("", -10000, 0)]
            [InlineData("", int.MinValue, 0)]
            [InlineData("", 1, 0)]
            [InlineData("", 10000, 0)]
            [InlineData("", int.MaxValue, 0)]
            [InlineData("012345", -1, 0)]
            [InlineData("012345", int.MinValue, 0)]
            [InlineData("012345", 7, 0)]
            [InlineData("012345", int.MaxValue, 0)]
            public void CtorRejectsStartIndexOutOfRange(string data, int startIndex, int length)
            {
                Assert.Throws<ArgumentOutOfRangeException>(
                    "startIndex",
                    () => new TextSourceCtors(data.ToCharArray(), startIndex, length));
            }

            private class TextSourceCtors : TextSource
            {
                public TextSourceCtors(char[] data)
                    : base(data)
                {
                }

                public TextSourceCtors(char[] data, int startIndex)
                    : base(data, startIndex)
                {
                }

                public TextSourceCtors(char[] data, int startIndex, int length)
                    : base(data, startIndex, length)
                {
                }

                public TextSourceCtors(int capacity)
                    : base(capacity)
                {
                }

                public override Encoding Encoding { get; }

                protected override int ReadImpl(char[] buffer, int startIndex, int maxCount)
                {
                    throw new NotImplementedException();
                }
            }
        }
    }
}
