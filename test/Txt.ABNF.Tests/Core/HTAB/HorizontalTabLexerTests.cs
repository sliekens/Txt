using Txt;
using Xunit;

namespace Text.ABNF.Core.HTAB
{
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
            using (var scanner = new TextScanner(new StringTextSource(input)))
            {
                var result = lexer.Read(scanner);
                Assert.NotNull(result);
                Assert.True(result.Success);
                Assert.NotNull(result.Element);
                Assert.Equal(input, result.Element.Text);
            }
        }
    }
}
