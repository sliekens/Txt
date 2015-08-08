namespace TextFx.ABNF.Core
{
    using Xunit;

    public class ControlCharacterLexerTests
    {
        [Theory]
        [InlineData("\x00")]
        [InlineData("\x10")]
        [InlineData("\x1F")]
        [InlineData("\x7F")]
        public void ReadSuccess(string input)
        {
            var factory = new ControlCharacterLexerFactory(new ValueRangeLexerFactory(), new TerminalLexerFactory(), new AlternativeLexerFactory());
            var controlCharacterLexer = factory.Create();
            using (var scanner = new TextScanner(input.ToMemoryStream()))
            {
                scanner.Read();
                var controlCharacter = controlCharacterLexer.Read(scanner, null);
                Assert.NotNull(controlCharacter);
                Assert.Equal(input, controlCharacter.Text);
            }
        }
    }
}
