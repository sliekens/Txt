using Txt.Core;
using Xunit;

namespace Txt.ABNF.Core.VCHAR
{
    public class VisibleCharacterLexerTests
    {
        [Theory]
        [InlineData("\x21")]
        [InlineData("\x7E")]
        public void ReadSuccess(string input)
        {
            var grammar = new CoreGrammar();
            grammar.Initialize();
            var sut = grammar.Rule("VCHAR");
            using (var scanner = new TextScanner(new StringTextSource(input)))
            {
                var result = sut.Read(scanner);
                Assert.Equal(input, result.Text);
            }
        }
    }
}
