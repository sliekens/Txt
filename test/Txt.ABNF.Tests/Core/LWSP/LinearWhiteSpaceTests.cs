using Txt.ABNF.Core.CR;
using Txt.ABNF.Core.CRLF;
using Txt.ABNF.Core.HTAB;
using Txt.ABNF.Core.LF;
using Txt.ABNF.Core.SP;
using Txt.ABNF.Core.WSP;
using Xunit;

namespace Txt.ABNF.Core.LWSP
{
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
            var alternativeLexerFactory = new AlternationLexerFactory();
            var sequenceLexerFactory = new ConcatenationLexerFactory();
            var repetitionLexerFactory = new RepetitionLexerFactory();

            // SP
            var spaceLexerFactory = new SpaceLexerFactory(terminalLexerFactory);

            // HTAB
            var horizontalTabLexerFactory = new HorizontalTabLexerFactory(terminalLexerFactory);

            // WSP
            var whiteSpaceLexerFactory = new WhiteSpaceLexerFactory(alternativeLexerFactory, spaceLexerFactory.Create(), horizontalTabLexerFactory.Create());

            // CR
            var carriageReturnLexerFactory = new CarriageReturnLexerFactory(terminalLexerFactory);

            // LF
            var lineFeedLexerFactory = new LineFeedLexerFactory(terminalLexerFactory);

            // CRLF
            var endOfLineLexerFactory = new EndOfLineLexerFactory(sequenceLexerFactory, carriageReturnLexerFactory.Create(), lineFeedLexerFactory.Create());

            // LWSP
            var linearWhiteSpaceLexerFactory = new LinearWhiteSpaceLexerFactory(alternativeLexerFactory,
                sequenceLexerFactory, repetitionLexerFactory, whiteSpaceLexerFactory.Create(), endOfLineLexerFactory.Create());
            var linearWhiteSpaceLexer = linearWhiteSpaceLexerFactory.Create();
            using (var scanner = new TextScanner(new StringTextSource(input)))
            {
                var result = linearWhiteSpaceLexer.Read(scanner);
                Assert.NotNull(result);
                Assert.True(result.Success);
                Assert.NotNull(result.Element);
                Assert.Equal(wellFormed, result.Element.GetWellFormedText());
            }
        }
    }
}