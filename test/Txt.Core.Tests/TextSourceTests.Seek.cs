using System;
using Xunit;

namespace Txt.Core
{
    public partial class TextSourceTests
    {
        public class Seek
        {
            [Theory]
            [InlineData(-1)]
            [InlineData(-2)]
            [InlineData(-10)]
            [InlineData(int.MinValue)]
            [InlineData(long.MinValue)]
            public void NegativeOffset(long offset)
            {
                // Given that there is a text source
                //  and the text length is 3
                // When Seek is called with a negative offset
                // Then an ArgumentOutOfRangeException is thrown
                using (var sut = new StringTextSource("abc"))
                {
                    Assert.Throws<ArgumentOutOfRangeException>(() => sut.Seek(offset));
                }
            }

            [Fact]
            public void SimpleForwardSeek()
            {
                // Given that there is a text source
                //  and the text length is 3
                // When Seek is called
                //  and the seek offset is 2
                // Then the current offset becomes 2
                using (var sut = new StringTextSource("abc"))
                {
                    sut.Seek(2);
                    Assert.Equal(2, sut.Offset);
                }
            }
        }
    }
}
