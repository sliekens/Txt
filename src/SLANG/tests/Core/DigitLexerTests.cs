namespace SLANG.Core
{
    using Xunit;

    public class DigitLexerTests
    {
        private readonly DigitLexer lexer = new DigitLexer();

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
        public void CanReadDigit(string digit)
        {
            using (ITextScanner textScanner = new TextScanner(new PushbackInputStream(digit.ToMemoryStream())))
            {
                textScanner.Read();
                var alpha = this.lexer.Read(textScanner);
                Assert.Equal(digit, alpha.Data);
            }
        }
    }
}
