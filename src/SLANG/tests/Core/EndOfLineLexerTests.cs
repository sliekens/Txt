namespace SLANG.Core
{
    using Xunit;

    public class EndOfLineLexerTests
    {
        [Theory]
        [InlineData("\r\n")]
        public void ReadSuccess(string input)
        {
            var endOfLineLexer = new EndOfLineLexer(new EndOfLineSequenceLexer(new CarriageReturnLexer(new CarriageReturnTerminalLexer()), new LineFeedLexer(new LineFeedTerminalLexer())));
            using (var scanner = new TextScanner(new PushbackInputStream(input.ToMemoryStream())))
            {
                var endOfLine = endOfLineLexer.Read(scanner);
                Assert.NotNull(endOfLine);
                Assert.Equal(input, endOfLine.Data);
            }
        }
    }
}
