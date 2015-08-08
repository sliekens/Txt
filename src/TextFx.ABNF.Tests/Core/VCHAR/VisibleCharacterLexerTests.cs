namespace TextFx.ABNF.Core
{
    using Xunit;

    public class VisibleCharacterLexerTests
    {
        [Theory]
        [InlineData("\x21")]
        [InlineData("\x7E")]
        public void ReadSuccess(string input)
        {
            var factory = new VisibleCharacterLexerFactory(new ValueRangeLexerFactory());
            var visibleCharacterLexer = factory.Create();
            using (var scanner = new TextScanner(input.ToMemoryStream()))
            {
                scanner.Read();
                var visibleCharacter = visibleCharacterLexer.Read(scanner, null);
                Assert.NotNull(visibleCharacter);
                Assert.Equal(input, visibleCharacter.Value);
            }
        }
    }
}
