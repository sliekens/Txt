namespace TextFx.ABNF.Core
{
    using Xunit;

    public class LineFeedLexerTests
    {
        [Theory]
        [InlineData("\x0A")]
        public void ReadSuccess(string input)
        {
            var factory = new LineFeedLexerFactory(new TerminalLexerFactory());
            var lineFeedLexer = factory.Create();
            using (var scanner = new TextScanner(new StringTextSource(input)))
            {
                scanner.Read();
                var lineFeed = lineFeedLexer.Read(scanner, null);
                Assert.NotNull(lineFeed);
                Assert.Equal(input, lineFeed.Text);
            }
        }
    }
}
