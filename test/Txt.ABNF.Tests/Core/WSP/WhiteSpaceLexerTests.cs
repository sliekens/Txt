using Txt.ABNF.Core.HTAB;
using Txt.ABNF.Core.SP;
using Xunit;

namespace Txt.ABNF.Core.WSP
{
    public class WhiteSpaceLexerTests
    {
        [Theory]
        [InlineData("\x20")]
        [InlineData(" ")]
        [InlineData("\x09")]
        [InlineData("\t")]
        [InlineData(@"	")]
        public void ReadSuccess(string input)
        {
            var terminalLexerFactory = new TerminalLexerFactory();
            var spaceLexerFactory = new SpaceLexerFactory(terminalLexerFactory);
            var horizontalTabLexerFactory = new HorizontalTabLexerFactory(terminalLexerFactory);
            var alternativeLexerFactory = new AlternativeLexerFactory();
            var whiteSpaceLexerFactory = new WhiteSpaceLexerFactory(spaceLexerFactory, horizontalTabLexerFactory, alternativeLexerFactory);
            var whiteSpaceLexer = whiteSpaceLexerFactory.Create();
            using (var scanner = new TextScanner(new StringTextSource(input)))
            {
                var result = whiteSpaceLexer.Read(scanner);
                Assert.NotNull(result);
                Assert.True(result.Success);
                Assert.NotNull(result.Element);
                Assert.Equal(input, result.Element.Text);
            }
        }
    }
}
