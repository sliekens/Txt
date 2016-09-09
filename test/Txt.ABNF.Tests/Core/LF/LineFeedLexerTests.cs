using Txt.Core;
using Xunit;

namespace Txt.ABNF.Core.LF
{
    public class LineFeedLexerTests
    {
        [Theory]
        [InlineData("\x0A")]
        public void ReadSuccess(string input)
        {
            var factory = new LineFeedLexerFactory(new TerminalLexerFactory());
            var lineFeedLexer = factory.Create();
            using (var scanner = new TextScanner(new StringTextSource(input)))
            {
                var result = lineFeedLexer.Read(scanner);
                Assert.Equal(input, result.Text);
            }
        }
    }
}
