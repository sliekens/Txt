namespace TextFx.ABNF.Core
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
            using (var scanner = new TextScanner(new StringTextSource(input)))
            {
                scanner.Read();
                var space = spaceLexer.Read(scanner, null);
                Assert.NotNull(space);
                Assert.Equal(input, space.Text);
            }
        }
    }
}
