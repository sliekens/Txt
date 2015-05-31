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
            var valueRangeLexer = new ValueRangeLexer('\x00', '\x1F');
            var terminalsLexer = new TerminalsLexer('\x7F');
            var controlCharacterAlternativeLexer = new AlternativeLexer(valueRangeLexer, terminalsLexer);
            var controlCharacterLexer = new ControlCharacterLexer(controlCharacterAlternativeLexer);
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
