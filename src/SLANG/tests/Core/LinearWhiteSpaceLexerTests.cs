namespace SLANG.Core
{
    using Xunit;

    public class LinearWhiteSpaceLexerTests
    {
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("   ")]
        [InlineData("\t")]
        [InlineData("\r\n\t")]
        [InlineData("\r\n   \t")]
        [InlineData("\t\r\n \t \r\n  \t ")]
        public void ReadSuccess(string input)
        {
            var spaceTerminalLexer = new TerminalsLexer('\x20');
            var spaceLexer = new SpaceLexer(spaceTerminalLexer);
            var horizontalTabTerminalLexer = new TerminalsLexer('\x09');
            var horizontalTabLexer = new HorizontalTabLexer(horizontalTabTerminalLexer);
            var whiteSpaceAlternativeLexer = new AlternativeLexer(spaceLexer, horizontalTabLexer);
            var whiteSpaceLexer = new WhiteSpaceLexer(whiteSpaceAlternativeLexer);
            var carriageReturnTerminalLexer = new TerminalsLexer('\x0D');
            var carriageReturnLexer = new CarriageReturnLexer(carriageReturnTerminalLexer);
            var lineFeedTerminalLexer = new TerminalsLexer('\x0A');
            var lineFeedLexer = new LineFeedLexer(lineFeedTerminalLexer);
            var endOfLineSequenceLexer = new SequenceLexer(carriageReturnLexer, lineFeedLexer);
            var endOfLineLexer = new EndOfLineLexer(endOfLineSequenceLexer);
            var endOfLineWhiteSpaceSequenceLexer = new EndOfLineWhiteSpaceSequenceLexer(endOfLineLexer, whiteSpaceLexer);
            var breakingWhiteSpaceLexer = new BreakingWhiteSpaceLexer(whiteSpaceLexer, endOfLineWhiteSpaceSequenceLexer);
            var linearWhiteSpaceRepetitionLexer = new RepetitionLexer(breakingWhiteSpaceLexer, 0, int.MaxValue);
            var lexer = new LinearWhiteSpaceLexer(linearWhiteSpaceRepetitionLexer);
            using (var scanner = new TextScanner(new PushbackInputStream(input.ToMemoryStream())))
            {
                scanner.Read();
                var linearWhiteSpace = lexer.Read(scanner);
                Assert.NotNull(lineFeedLexer);
                Assert.Equal(input, linearWhiteSpace.Data);
            }
        }
    }
}
