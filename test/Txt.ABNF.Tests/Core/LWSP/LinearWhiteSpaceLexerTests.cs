using Txt.Core;
using Txt.ABNF.Core.CR;
using Txt.ABNF.Core.CRLF;
using Txt.ABNF.Core.HTAB;
using Txt.ABNF.Core.LF;
using Txt.ABNF.Core.SP;
using Txt.ABNF.Core.WSP;
using Xunit;

namespace Txt.ABNF.Core.LWSP
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
            var newLineLexerFactory = new NewLineLexerFactory(sequenceLexerFactory, carriageReturnLexerFactory.Create(), lineFeedLexerFactory.Create());

            // LWSP
            var linearWhiteSpaceLexerFactory = new LinearWhiteSpaceLexerFactory(alternativeLexerFactory,
                sequenceLexerFactory, repetitionLexerFactory, whiteSpaceLexerFactory.Create(), newLineLexerFactory.Create());
            var linearWhiteSpaceLexer = linearWhiteSpaceLexerFactory.Create();
            using (var scanner = new TextScanner(new StringTextSource(input)))
            {
                var result = linearWhiteSpaceLexer.Read(scanner);
                Assert.NotNull(result);
                Assert.True(result.IsSuccess);
                Assert.NotNull(result.Element);
                Assert.Equal(input, result.Element.Text);
            }
        }
    }
}