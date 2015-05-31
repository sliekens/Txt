namespace SLANG.Core
{
    using SLANG.Core.CR;

    using Xunit;

    public class CarriageReturnLexerTests
    {
        [Theory]
        [InlineData("\x0D")]
        public void ReadSuccess(string input)
        {
            var factory = new CarriageReturnLexerFactory(new TerminalsLexerFactory());
            var carriageReturnLexer = factory.Create();
            using (var scanner = new TextScanner(new PushbackInputStream(input.ToMemoryStream())))
            {
                scanner.Read();
                var carriageReturn = carriageReturnLexer.Read(scanner);
                Assert.NotNull(carriageReturn);
                Assert.Equal(input, carriageReturn.Data);
            }
        }
    }
}
