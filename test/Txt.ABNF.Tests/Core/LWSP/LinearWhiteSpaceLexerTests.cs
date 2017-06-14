using Txt.Core;
using Xunit;

namespace Txt.ABNF.Core.LWSP
{
    public class LinearWhiteSpaceLexerTests
    {
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("   ")]
        [InlineData("\t")]
        [InlineData("\r\n\t")]
        [InlineData("\r\n   \t")]
        [InlineData("\t\r\n \t \r\n  \t ")]
        public void ReadSuccess(string input)
        {
            var grammar = new CoreGrammar();
            grammar.Initialize();
            var sut = grammar.Rule("LWSP");
            using (var scanner = new TextScanner(new StringTextSource(input)))
            {
                var result = sut.Read(scanner);
                Assert.Equal(input, result.Text);
            }
        }
    }
}
