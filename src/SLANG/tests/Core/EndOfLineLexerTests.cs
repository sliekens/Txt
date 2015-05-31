namespace SLANG.Core
{
    using SLANG.Core.CR;
    using SLANG.Core.CRLF;
    using SLANG.Core.LF;

    using Xunit;

    public class EndOfLineLexerTests
    {
        [Theory]
        [InlineData("\r\n")]
        public void ReadSuccess(string input)
        {
            var terminalsLexerFactory = new TerminalsLexerFactory();
            var sequenceLexerFactory = new SequenceLexerFactory();
            var carriageReturnLexerFactory = new CarriageReturnLexerFactory(terminalsLexerFactory);
            var lineFeedLexerFactory = new LineFeedLexerFactory(terminalsLexerFactory);
            var carriageReturnLexer = carriageReturnLexerFactory.Create();
            var lineFeedLexer = lineFeedLexerFactory.Create();
            var factory = new EndOfLineLexerFactory(carriageReturnLexer, lineFeedLexer, sequenceLexerFactory);
            var endOfLineLexer = factory.Create();
            using (var scanner = new TextScanner(new PushbackInputStream(input.ToMemoryStream())))
            {
                scanner.Read();
                var endOfLine = endOfLineLexer.Read(scanner);
                Assert.NotNull(endOfLine);
                Assert.Equal(input, endOfLine.Data);
            }
        }
    }
}
