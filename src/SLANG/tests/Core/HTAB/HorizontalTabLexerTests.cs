namespace SLANG.Core.HTAB
{
    using SLANG.Core.DQUOTE;

    using Xunit;

    public class HorizontalTabLexerTests
    {
        [Theory]
        [InlineData("\x09")]
        [InlineData("\t")]
        public void ReadSuccess(string input)
        {
            var factory = new HorizontalTabLexerFactory(new TerminalLexerFactory());
            var lexer = factory.Create();
            using (var scanner = new TextScanner(new PushbackInputStream(input.ToMemoryStream())))
            {
                scanner.Read();
                var horizontalTab = lexer.Read(scanner);
                Assert.Equal(input, horizontalTab.Data);
            }
        }
    }
}
