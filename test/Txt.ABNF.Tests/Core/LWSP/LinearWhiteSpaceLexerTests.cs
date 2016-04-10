using Text.ABNF.Core.CR;
using Text.ABNF.Core.CRLF;
using Text.ABNF.Core.HTAB;
using Text.ABNF.Core.LF;
using Text.ABNF.Core.SP;
using Text.ABNF.Core.WSP;
using Txt;
using Xunit;

namespace Text.ABNF.Core.LWSP
{
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
            // General
            var terminalLexerFactory = new TerminalLexerFactory();
            var alternativeLexerFactory = new AlternativeLexerFactory();
            var sequenceLexerFactory = new ConcatenationLexerFactory();
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
                var result = linearWhiteSpaceLexer.Read(scanner);
                Assert.NotNull(result);
                Assert.True(result.Success);
                Assert.NotNull(result.Element);
                Assert.Equal(input, result.Element.Text);
            }
        }
    }
}