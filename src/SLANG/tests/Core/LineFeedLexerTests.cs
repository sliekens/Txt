namespace SLANG.Core
{
    using Xunit;

    public class LineFeedLexerTests
    {
        [Theory]
        [InlineData("\x0A")]
        public void ReadSuccess(string input)
        {
            var lineFeedLexer = new LineFeedLexer(new LineFeedTerminalLexer());
            using (var scanner = new TextScanner(new PushbackInputStream(input.ToMemoryStream())))
            {
                var lineFeed = lineFeedLexer.Read(scanner);
                Assert.NotNull(lineFeed);
                Assert.Equal(input, lineFeed.Data);
            }
        }
    }
}
