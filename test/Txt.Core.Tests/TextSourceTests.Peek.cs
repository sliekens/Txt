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
                // Given that there is a text source
                //  and the text length is 0
                //  and the current offset is 0
                //  and the current line is 1
                //  and the current column is 1
                // When Peek is called
                // Then the result is -1
                //  and the current offset is 0
                //  and the current line is 1
                //  and the current column is 1
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
            public void SimplePeek(string data)
            {
                var sut = new StringTextSource(data);
                var result = (char)sut.Peek();
                Assert.Equal(data, char.ToString(result));
                Assert.Equal(0, sut.Offset);
                Assert.Equal(1, sut.Line);
                Assert.Equal(1, sut.Column);
            }
        }
    }
}
