using Txt.Core;
using Xunit;

namespace Txt.ABNF.Core.CRLF
{
    public class NewLineLexerTests
    {
        [Theory]
        [InlineData("\r\n")]
        public void ReadSuccess(string input)
        {
            var grammar = new CoreGrammar();
            grammar.Initialize();
            var sut = grammar.Rule("CRLF");
            using (var scanner = new TextScanner(new StringTextSource(input)))
            {
                var result = sut.Read(scanner);
                Assert.Equal(input, result.Text);
            }
        }
    }
}
