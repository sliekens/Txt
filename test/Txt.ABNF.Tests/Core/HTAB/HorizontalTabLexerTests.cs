using Txt.Core;
using Xunit;

namespace Txt.ABNF.Core.HTAB
{
    public class HorizontalTabLexerTests
    {
        [Theory]
        [InlineData("\x09")]
        [InlineData("\t")]
        [InlineData(@"	")]
        public void ReadSuccess(string input)
        {
            var grammar = new CoreGrammar();
            grammar.Initialize();
            var sut = grammar.Rule("HTAB");
            using (var scanner = new TextScanner(new StringTextSource(input)))
            {
                var result = sut.Read(scanner);
                Assert.Equal(input, result.Text);
            }
        }
    }
}
