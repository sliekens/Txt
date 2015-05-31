namespace SLANG.Core
{
    using Xunit;

    public class CarriageReturnLexerTests
    {
        [Theory]
        [InlineData("\x0D")]
        public void ReadSuccess(string input)
        {
            var carriageReturnTerminalLexer = new TerminalsLexer('\x0D');
            var carriageReturnLexer = new CarriageReturnLexer(carriageReturnTerminalLexer);
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
