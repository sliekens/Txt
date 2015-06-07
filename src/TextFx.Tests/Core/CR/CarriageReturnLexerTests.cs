namespace TextFx.Core
{
    using Xunit;

    public class CarriageReturnLexerTests
    {
        [Theory]
        [InlineData("\x0D")]
        public void ReadSuccess(string input)
        {
            var factory = new CarriageReturnLexerFactory(new TerminalLexerFactory());
            var carriageReturnLexer = factory.Create();
            using (var scanner = new TextScanner(new PushbackInputStream(input.ToMemoryStream())))
            {
                scanner.Read();
                var carriageReturn = carriageReturnLexer.Read(scanner);
                Assert.NotNull(carriageReturn);
                Assert.Equal(input, carriageReturn.Values);
            }
        }
    }
}
