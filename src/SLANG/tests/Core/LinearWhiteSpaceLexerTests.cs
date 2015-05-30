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
        public void ReadSuccess(string input)
        {
            var spaceTerminalLexer = new SpaceTerminalLexer();
            var spaceLexer = new SpaceLexer(spaceTerminalLexer);
            var horizontalTabTerminalLexer = new HorizontalTabTerminalLexer();
            var horizontalTabLexer = new HorizontalTabLexer(horizontalTabTerminalLexer);
            var whiteSpaceAlternativeLexer = new WhiteSpaceAlternativeLexer(spaceLexer, horizontalTabLexer);
            var whiteSpaceLexer = new WhiteSpaceLexer(whiteSpaceAlternativeLexer);
            var carriageReturnTerminalLexer = new CarriageReturnTerminalLexer();
            var carriageReturnLexer = new CarriageReturnLexer(carriageReturnTerminalLexer);
            var lineFeedTerminalLexer = new LineFeedTerminalLexer();
            var lineFeedLexer = new LineFeedLexer(lineFeedTerminalLexer);
            var endOfLineSequenceLexer = new EndOfLineSequenceLexer(carriageReturnLexer, lineFeedLexer);
            var endOfLineLexer = new EndOfLineLexer(endOfLineSequenceLexer);
            var endOfLineWhiteSpaceSequenceLexer = new EndOfLineWhiteSpaceSequenceLexer(endOfLineLexer, whiteSpaceLexer);
            var breakingWhiteSpaceLexer = new BreakingWhiteSpaceLexer(whiteSpaceLexer, endOfLineWhiteSpaceSequenceLexer);
            var linearWhiteSpaceRepetitionLexer = new LinearWhiteSpaceRepetitionLexer(breakingWhiteSpaceLexer);
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
