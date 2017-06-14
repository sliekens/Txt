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
            var grammar = new CoreGrammar();
            grammar.Initialize();
            var sut = grammar.Rule("DQUOTE");
            using (var scanner = new TextScanner(new StringTextSource(input)))
            {
                var result = sut.Read(scanner);
                Assert.Equal(input, result.Text);
            }
        }
    }
}
