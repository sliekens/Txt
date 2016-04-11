using Xunit;

namespace Txt.ABNF.Core.SP
{
    public class SpaceLexerTests
    {
        [Theory]
        [InlineData("\x20")]
        [InlineData(" ")]
        public void ReadSuccess(string input)
        {
            var factory = new SpaceLexerFactory(new TerminalLexerFactory());
            var spaceLexer = factory.Create();
            using (var scanner = new TextScanner(new StringTextSource(input)))
            {
                var result = spaceLexer.Read(scanner);
                Assert.NotNull(result);
                Assert.True(result.Success);
                Assert.NotNull(result.Element);
                Assert.Equal(input, result.Element.Text);
            }
        }
    }
}
