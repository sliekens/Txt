namespace TextFx.ABNF.Core
{
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
            using (var scanner = new TextScanner(input.ToMemoryStream()))
            {
                scanner.Read();
                var doubleQuote = lexer.Read(scanner, null);
                Assert.Equal(input, doubleQuote.Value);
            }
        }
    }
}
