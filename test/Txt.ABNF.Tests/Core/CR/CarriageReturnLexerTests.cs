using Txt.Core;
using Xunit;

namespace Txt.ABNF.Core.CR
{
    public class CarriageReturnLexerTests
    {
        [Theory]
        [InlineData("\x0D")]
        public void ReadSuccess(string input)
        {
            var grammar = new CoreGrammar();
            grammar.Initialize();
            var sut = grammar.Rule("CR");
            using (var scanner = new TextScanner(new StringTextSource(input)))
            {
                var result = sut.Read(scanner);
                Assert.Equal(input, result.Text);
            }
        }
    }
}
