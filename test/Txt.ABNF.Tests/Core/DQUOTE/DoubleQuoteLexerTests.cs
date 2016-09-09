using Txt.Core;
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
                Assert.Equal(input, result.Text);
            }
        }
    }
}
