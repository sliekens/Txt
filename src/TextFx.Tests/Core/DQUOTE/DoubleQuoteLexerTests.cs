namespace TextFx.Core
{
    using TextFx.ABNF;
    using TextFx.ABNF.Core;

    using Xunit;

    public class DoubleQuoteLexerTests
    {
        [Theory]
        [InlineData("\"")]
        [InlineData("\x22")]
        public void ReadSuccess(string input)
        {
            var factory = new DoubleQuoteLexerFactory(new TerminalLexerFactory());
            var lexer = factory.Create();
            using (var scanner = new TextScanner(new PushbackInputStream(input.ToMemoryStream())))
            {
                scanner.Read();
                var doubleQuote = lexer.Read(scanner);
                Assert.Equal(input, doubleQuote.Values);
            }
        }
    }
}
