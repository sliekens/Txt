namespace SLANG.Core
{
    using Xunit;

    public class EndOfLineLexerTests
    {
        [Theory]
        [InlineData("\r\n")]
        public void ReadSuccess(string input)
        {
            var carriageReturnTerminalLexer = new TerminalsLexer('\x0D');
            var carriageReturnLexer = new CarriageReturnLexer(carriageReturnTerminalLexer);
            var lineFeedTerminalLexer = new TerminalsLexer('\x0A');
            var lineFeedLexer = new LineFeedLexer(lineFeedTerminalLexer);
            var endOfLineSequenceLexer = new EndOfLineSequenceLexer(carriageReturnLexer, lineFeedLexer);
            var endOfLineLexer = new EndOfLineLexer(endOfLineSequenceLexer);
            using (var scanner = new TextScanner(new PushbackInputStream(input.ToMemoryStream())))
            {
                scanner.Read();
                var endOfLine = endOfLineLexer.Read(scanner);
                Assert.NotNull(endOfLine);
                Assert.Equal(input, endOfLine.Data);
            }
        }
    }
}
