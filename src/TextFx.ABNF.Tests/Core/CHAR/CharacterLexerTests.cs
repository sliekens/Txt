namespace TextFx.ABNF.Core
{
    using Xunit;

    public class CharacterLexerTests
    {
        [Theory]
        [InlineData("\x01")]
        [InlineData("\x10")]
        [InlineData("\x20")]
        [InlineData("\x30")]
        [InlineData("\x40")]
        [InlineData("\x50")]
        [InlineData("\x60")]
        [InlineData("\x70")]
        [InlineData("\x7F")]
        public void ReadSuccess(string input)
        {
            var factory = new CharacterLexerFactory(new ValueRangeLexerFactory());
            var lexer = factory.Create();
            using (var scanner = new TextScanner(input.ToMemoryStream()))
            {
                scanner.Read();
                var character = lexer.Read(scanner, null);
                Assert.NotNull(character);
                Assert.Equal(input, character.Value);
            }
        }
    }
}
