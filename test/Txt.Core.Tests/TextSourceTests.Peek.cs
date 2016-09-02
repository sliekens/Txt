using Xunit;

namespace Txt.Core
{
    public partial class TextSourceTests
    {
        public class Peek
        {
            [Fact]
            public void EmptyData()
            {
                var sut = new StringTextSource("");
                Assert.Equal(-1, sut.Peek());
                Assert.Equal(0, sut.Offset);
                Assert.Equal(1, sut.Line);
                Assert.Equal(1, sut.Column);
            }

            [Theory]
            [InlineData("0")]
            [InlineData("9")]
            [InlineData("!")]
            [InlineData("A")]
            [InlineData("Z")]
            [InlineData("ù")]
            [InlineData("☕")]
            [InlineData("\0")]
            [InlineData("\uFFFF")]
            public void NonEmptyData(string data)
            {
                var sut = new StringTextSource(data);
                Assert.Equal(data, char.ToString((char)sut.Peek()));
                Assert.Equal(0, sut.Offset);
                Assert.Equal(1, sut.Line);
                Assert.Equal(1, sut.Column);
            }
        }
    }
}
