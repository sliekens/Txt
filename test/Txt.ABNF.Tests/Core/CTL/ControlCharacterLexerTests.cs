using Txt.Core;
using Xunit;

namespace Txt.ABNF.Core.CTL
{
    public class ControlCharacterLexerTests
    {
        [Theory]
        [InlineData("\x00")]
        [InlineData("\x10")]
        [InlineData("\x1F")]
        [InlineData("\x7F")]
        public void ReadSuccess(string input)
        {
            var grammar = new CoreGrammar();
            grammar.Initialize();
            var sut = grammar.Rule("CTL");
            using (var scanner = new TextScanner(new StringTextSource(input)))
            {
                var result = sut.Read(scanner);
                Assert.Equal(input, result.Text);
            }
        }
    }
}
