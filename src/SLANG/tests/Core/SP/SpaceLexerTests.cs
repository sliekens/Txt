namespace SLANG.Core.SP
{
    using Xunit;

    public class SpaceLexerTests
    {
        [Theory]
        [InlineData("\x20")]
        [InlineData(" ")]
        public void ReadSuccess(string input)
        {
            var factory = new SpaceLexerFactory(new TerminalLexerFactory());
            var spaceLexer = factory.Create();
            using (var scanner = new TextScanner(new PushbackInputStream(input.ToMemoryStream())))
            {
                scanner.Read();
                var space = spaceLexer.Read(scanner);
                Assert.NotNull(space);
                Assert.Equal(input, space.Values);
            }
        }
    }
}
