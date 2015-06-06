namespace SLANG.Core
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
            using (var scanner = new TextScanner(new PushbackInputStream(input.ToMemoryStream())))
            {
                scanner.Read();
                var visibleCharacter = visibleCharacterLexer.Read(scanner);
                Assert.NotNull(visibleCharacter);
                Assert.Equal(input, visibleCharacter.Values);
            }
        }
    }
}
