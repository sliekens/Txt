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
            // SP
            var spaceTerminalLexer = new TerminalsLexer('\x20');
            var spaceLexer = new SpaceLexer(spaceTerminalLexer);

            // HTAB
            var horizontalTabTerminalLexer = new TerminalsLexer('\x09');
            var horizontalTabLexer = new HorizontalTabLexer(horizontalTabTerminalLexer);

            // WSP
            var whiteSpaceAlternativeLexer = new AlternativeLexer(spaceLexer, horizontalTabLexer);
            var whiteSpaceLexer = new WhiteSpaceLexer(whiteSpaceAlternativeLexer);

            // CR
            var carriageReturnTerminalLexer = new TerminalsLexer('\x0D');
            var carriageReturnLexer = new CarriageReturnLexer(carriageReturnTerminalLexer);

            // LF
            var lineFeedTerminalLexer = new TerminalsLexer('\x0A');
            var lineFeedLexer = new LineFeedLexer(lineFeedTerminalLexer);

            // CRLF
            var endOfLineSequenceLexer = new SequenceLexer(carriageReturnLexer, lineFeedLexer);
            var endOfLineLexer = new EndOfLineLexer(endOfLineSequenceLexer);

            // CRLF WSP
            var endOfLineWhiteSpaceSequenceLexer = new SequenceLexer(endOfLineLexer, whiteSpaceLexer);

            // WSP / CRLF WSP
            var breakingWhiteSpaceLexer = new AlternativeLexer(whiteSpaceLexer, endOfLineWhiteSpaceSequenceLexer);

            // *( WSP / CRLF WSP )
            var linearWhiteSpaceRepetitionLexer = new RepetitionLexer(breakingWhiteSpaceLexer, 0, int.MaxValue);

            // LWSP
            var lexer = new LinearWhiteSpaceLexer(linearWhiteSpaceRepetitionLexer);
            using (var scanner = new TextScanner(new PushbackInputStream(input.ToMemoryStream())))
            {
                scanner.Read();
                var linearWhiteSpace = lexer.Read(scanner);
                Assert.NotNull(linearWhiteSpace);
                Assert.Equal(input, linearWhiteSpace.Data);
            }
        }
    }
}
