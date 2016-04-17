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
            var factory = new ControlCharacterLexerFactory(new ValueRangeLexerFactory(), new TerminalLexerFactory(), new AlternationLexerFactory());
            var controlCharacterLexer = factory.Create();
            using (var scanner = new TextScanner(new StringTextSource(input)))
            {
                var result = controlCharacterLexer.Read(scanner);
                Assert.NotNull(result);
                Assert.True(result.Success);
                Assert.NotNull(result.Element);
                Assert.Equal(input, result.Element.Text);
            }
        }
    }
}
