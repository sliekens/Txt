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
            var grammar = new CoreGrammar();
            grammar.Initialize();
            var sut = grammar.Rule("LF");
            using (var scanner = new TextScanner(new StringTextSource(input)))
            {
                var result = sut.Read(scanner);
                Assert.Equal(input, result.Text);
            }
        }
    }
}
