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
            var factory = new VisibleCharacterLexerFactory(new ValueRangeLexerFactory());
            var visibleCharacterLexer = factory.Create();
            using (var scanner = new TextScanner(new StringTextSource(input)))
            {
                var result = visibleCharacterLexer.Read(scanner);
                Assert.Equal(input, result.Text);
            }
        }
    }
}
