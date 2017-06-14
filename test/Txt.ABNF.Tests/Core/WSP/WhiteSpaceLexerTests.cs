using Txt.Core;
using Xunit;

namespace Txt.ABNF.Core.WSP
{
    public class WhiteSpaceLexerTests
    {
        [Theory]
        [InlineData("\x20")]
        [InlineData(" ")]
        [InlineData("\x09")]
        [InlineData("\t")]
        [InlineData(@"	")]
        public void ReadSuccess(string input)
        {
            var grammar = new CoreGrammar();
            grammar.Initialize();
            var sut = grammar.Rule("WSP");
            using (var scanner = new TextScanner(new StringTextSource(input)))
            {
                var result = sut.Read(scanner);
                Assert.Equal(input, result.Text);
            }
        }
    }
}
