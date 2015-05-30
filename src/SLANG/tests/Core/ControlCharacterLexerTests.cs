namespace SLANG.Core
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
            var controlCharacterLexer = new ControlCharacterLexer(new ControlCharacterAlternativeLexer(new ControlsValueRangeLexer(), new DeleteTerminalLexer()));
            using (var scanner = new TextScanner(new PushbackInputStream(input.ToMemoryStream())))
            {
                scanner.Read();
                var controlCharacter = controlCharacterLexer.Read(scanner);
                Assert.NotNull(controlCharacter);
                Assert.Equal(input, controlCharacter.Data);
            }
        }
    }
}
