namespace SLANG.Core.CRLF
{
    using SLANG.Core.CR;
    using SLANG.Core.LF;

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
            using (var scanner = new TextScanner(new PushbackInputStream(input.ToMemoryStream())))
            {
                scanner.Read();
                var endOfLine = endOfLineLexer.Read(scanner);
                Assert.NotNull(endOfLine);
                Assert.Equal(input, endOfLine.Values);
            }
        }
    }
}
