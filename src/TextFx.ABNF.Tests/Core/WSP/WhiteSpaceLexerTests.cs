namespace TextFx.ABNF.Core
{
    using Xunit;

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
                var whiteSpace = whiteSpaceLexer.Read(scanner, null);
                Assert.NotNull(whiteSpace);
                Assert.Equal(input, whiteSpace.Text);
            }
        }
    }
}
