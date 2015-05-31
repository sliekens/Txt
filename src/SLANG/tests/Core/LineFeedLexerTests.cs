namespace SLANG.Core
{
    using Xunit;

    public class LineFeedLexerTests
    {
        [Theory]
        [InlineData("\x0A")]
        public void ReadSuccess(string input)
        {
            var lineFeedTerminalLexer = new TerminalsLexer('\x0A');
            var lineFeedLexer = new LineFeedLexer(lineFeedTerminalLexer);
            using (var scanner = new TextScanner(new PushbackInputStream(input.ToMemoryStream())))
            {
                scanner.Read();
                var lineFeed = lineFeedLexer.Read(scanner);
                Assert.NotNull(lineFeed);
                Assert.Equal(input, lineFeed.Data);
            }
        }
    }
}
