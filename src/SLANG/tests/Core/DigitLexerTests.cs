namespace SLANG.Core
{
    using Xunit;

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
            var valueRangeLexer = new ValueRangeLexer('\x30', '\x39');
            var digitLexer = new DigitLexer(valueRangeLexer);
            using (var scanner = new TextScanner(new PushbackInputStream(input.ToMemoryStream())))
            {
                scanner.Read();
                var digit = digitLexer.Read(scanner);
                Assert.Equal(input, digit.Data);
            }
        }
    }
}
