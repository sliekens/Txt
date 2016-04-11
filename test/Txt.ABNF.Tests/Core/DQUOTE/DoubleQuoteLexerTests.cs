using Xunit;

namespace Txt.ABNF.Core.DQUOTE
{
    public class DoubleQuoteLexerTests
    {
        [Theory]
        [InlineData("\"")]
        [InlineData("\x22")]
        public void ReadSuccess(string input)
        {
            var factory = new DoubleQuoteLexerFactory(new TerminalLexerFactory());
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
