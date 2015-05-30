namespace SLANG.Core
{
    using Xunit;

    public class CarriageReturnLexerTests
    {
        [Theory]
        [InlineData("\x0D")]
        public void ReadSuccess(string input)
        {
            var carriageReturnLexer = new CarriageReturnLexer(new CarriageReturnTerminalLexer());
            using (var scanner = new TextScanner(new PushbackInputStream(input.ToMemoryStream())))
            {
                var carriageReturn = carriageReturnLexer.Read(scanner);
                Assert.NotNull(carriageReturn);
                Assert.Equal(input, carriageReturn.Data);
            }
        }
    }
}
