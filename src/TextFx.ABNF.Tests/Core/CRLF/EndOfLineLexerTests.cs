namespace TextFx.ABNF.Core
{
    using Xunit;

    public class EndOfLineLexerTests
    {
        [Theory]
        [InlineData("\r\n")]
        public void ReadSuccess(string input)
        {
            var terminalsLexerFactory = new TerminalLexerFactory();
            var sequenceLexerFactory = new SequenceLexerFactory();
            var carriageReturnLexerFactory = new CarriageReturnLexerFactory(terminalsLexerFactory);
            var lineFeedLexerFactory = new LineFeedLexerFactory(terminalsLexerFactory);
            var factory = new EndOfLineLexerFactory(carriageReturnLexerFactory, lineFeedLexerFactory, sequenceLexerFactory);
            var endOfLineLexer = factory.Create();
            using (var scanner = new TextScanner(input.ToMemoryStream()))
            {
                scanner.Read();
                var endOfLine = endOfLineLexer.Read(scanner, null);
                Assert.NotNull(endOfLine);
                Assert.Equal(input, endOfLine.Value);
            }
        }
    }
}
