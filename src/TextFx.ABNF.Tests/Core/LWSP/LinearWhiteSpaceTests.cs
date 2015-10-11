namespace TextFx.ABNF.Core
{
    using Xunit;

    public class LinearWhiteSpaceTests
    {
        [Theory]
        [InlineData("", "")]
        [InlineData(" ", " ")]
        [InlineData("  ", " ")]
        [InlineData("\t", " ")]
        [InlineData("\t ", " ")]
        [InlineData("\t  ", " ")]
        [InlineData("\t   ", " ")]
        [InlineData("\r\n ", " ")]
        [InlineData("\r\n  ", " ")]
        [InlineData("\r\n   ", " ")]
        [InlineData("\r\n\t", " ")]
        [InlineData("\r\n\t\t", " ")]
        [InlineData("\r\n\t\t\t", " ")]
        [InlineData("\t\t\t\r\n\t", " ")]
        public void GetWellFormedValues_ShouldBehaveAsExpected(string input, string wellFormed)
        {
            // General
            var terminalLexerFactory = new TerminalLexerFactory();
            var alternativeLexerFactory = new AlternativeLexerFactory();
            var sequenceLexerFactory = new SequenceLexerFactory();
            var repetitionLexerFactory = new RepetitionLexerFactory();

            // SP
            var spaceLexerFactory = new SpaceLexerFactory(terminalLexerFactory);

            // HTAB
            var horizontalTabLexerFactory = new HorizontalTabLexerFactory(terminalLexerFactory);

            // WSP
            var whiteSpaceLexerFactory = new WhiteSpaceLexerFactory(
                spaceLexerFactory,
                horizontalTabLexerFactory,
                alternativeLexerFactory);

            // CR
            var carriageReturnLexerFactory = new CarriageReturnLexerFactory(terminalLexerFactory);

            // LF
            var lineFeedLexerFactory = new LineFeedLexerFactory(terminalLexerFactory);

            // CRLF
            var endOfLineLexerFactory = new EndOfLineLexerFactory(
                carriageReturnLexerFactory,
                lineFeedLexerFactory,
                sequenceLexerFactory);

            // LWSP
            var linearWhiteSpaceLexerFactory = new LinearWhiteSpaceLexerFactory(
                whiteSpaceLexerFactory,
                endOfLineLexerFactory,
                sequenceLexerFactory,
                alternativeLexerFactory,
                repetitionLexerFactory);
            var linearWhiteSpaceLexer = linearWhiteSpaceLexerFactory.Create();
            using (var scanner = new TextScanner(new StringTextSource(input)))
            {
                scanner.Read();
                var linearWhiteSpace = linearWhiteSpaceLexer.Read(scanner, null);
                Assert.Equal(wellFormed, linearWhiteSpace.GetWellFormedText());
            }
        }
    }
}