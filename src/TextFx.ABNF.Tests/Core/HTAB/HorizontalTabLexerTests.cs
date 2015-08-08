namespace TextFx.ABNF.Core
{
    using Xunit;

    public class HorizontalTabLexerTests
    {
        [Theory]
        [InlineData("\x09")]
        [InlineData("\t")]
        [InlineData(@"	")]
        public void ReadSuccess(string input)
        {
            var factory = new HorizontalTabLexerFactory(new TerminalLexerFactory());
            var lexer = factory.Create();
            using (var scanner = new TextScanner(input.ToMemoryStream()))
            {
                scanner.Read();
                var horizontalTab = lexer.Read(scanner, null);
                Assert.Equal(input, horizontalTab.Text);
            }
        }
    }
}
