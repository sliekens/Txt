using Txt.Core;
using Xunit;

namespace Txt.ABNF.Core.DIGIT
{
    public class DigitLexerTests
    {
        [Theory]
        [InlineData("\x30")]
        [InlineData("\x31")]
        [InlineData("\x32")]
        [InlineData("\x33")]
        [InlineData("\x34")]
        [InlineData("\x35")]
        [InlineData("\x36")]
        [InlineData("\x37")]
        [InlineData("\x38")]
        [InlineData("\x39")]
        public void ReadSuccess(string input)
        {
            var factory = new DigitLexerFactory(new ValueRangeLexerFactory());
            var digitLexer = factory.Create();
            using (var scanner = new TextScanner(new StringTextSource(input)))
            {
                var result = digitLexer.Read(scanner);
                Assert.NotNull(result);
                Assert.True(result.IsSuccess);
                Assert.NotNull(result.Element);
                Assert.Equal(input, result.Element.Text);
            }
        }
    }
}
