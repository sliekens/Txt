namespace SLANG.Core
{
    using SLANG.Core.LF;

    using Xunit;

    public class LineFeedLexerTests
    {
        [Theory]
        [InlineData("\x0A")]
        public void ReadSuccess(string input)
        {
            var factory = new LineFeedLexerFactory(new TerminalsLexerFactory());
            var lineFeedLexer = factory.Create();
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
