using Txt.Core;
using Xunit;

namespace Txt.ABNF.Core.SP
{
    public class SpaceLexerTests
    {
        [Theory]
        [InlineData("\x20")]
        [InlineData(" ")]
        public void ReadSuccess(string input)
        {
            var sut = SpaceLexerFactory.Default.Create();
            using (var scanner = new TextScanner(new StringTextSource(input)))
            {
                var result = sut.Read(scanner);
                Assert.Equal(input, result.Text);
            }
        }
    }
}
