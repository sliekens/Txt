namespace SLANG.Core
{
    using SLANG.Core.CR;
    using SLANG.Core.CRLF;
    using SLANG.Core.HTAB;
    using SLANG.Core.LF;
    using SLANG.Core.LWSP;
    using SLANG.Core.SP;
    using SLANG.Core.WSP;

    using Xunit;

    public class LinearWhiteSpaceLexerTests
    {
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("   ")]
        [InlineData("\t")]
        [InlineData("\r\n\t")]
        [InlineData("\r\n   \t")]
        [InlineData("\t\r\n \t \r\n  \t ")]
        public void ReadSuccess(string input)
        {
            // General
            var terminalsLexerFactory = new TerminalsLexerFactory();
            var alternativeLexerFactory = new AlternativeLexerFactory();
            var sequenceLexerFactory = new SequenceLexerFactory();
            var repetitionLexerFactory = new RepetitionLexerFactory();

            // SP
            var spaceLexerFactory = new SpaceLexerFactory(terminalsLexerFactory);
            var spaceLexer = spaceLexerFactory.Create();

            // HTAB
            var horizontalTabLexerFactory = new HorizontalTabLexerFactory(terminalsLexerFactory);
            var horizontalTabLexer = horizontalTabLexerFactory.Create();

            // WSP
            var whiteSpaceLexerFactory = new WhiteSpaceLexerFactory(
                spaceLexer,
                horizontalTabLexer,
                alternativeLexerFactory);
            var whiteSpaceLexer = whiteSpaceLexerFactory.Create();

            // CR
            var carriageReturnLexerFactory = new CarriageReturnLexerFactory(terminalsLexerFactory);
            var carriageReturnLexer = carriageReturnLexerFactory.Create();

            // LF
            var lineFeedLexerFactory = new LineFeedLexerFactory(terminalsLexerFactory);
            var lineFeedLexer = lineFeedLexerFactory.Create();

            // CRLF
            var endOfLineLexerFactory = new EndOfLineLexerFactory(
                carriageReturnLexer,
                lineFeedLexer,
                sequenceLexerFactory);
            var endOfLineLexer = endOfLineLexerFactory.Create();

            // LWSP
            var linearWhiteSpaceLexerFactory = new LinearWhiteSpaceLexerFactory(
                whiteSpaceLexer,
                endOfLineLexer,
                sequenceLexerFactory,
                alternativeLexerFactory,
                repetitionLexerFactory);
            var linearWhiteSpaceLexer = linearWhiteSpaceLexerFactory.Create();
            using (var scanner = new TextScanner(new PushbackInputStream(input.ToMemoryStream())))
            {
                scanner.Read();
                var linearWhiteSpace = linearWhiteSpaceLexer.Read(scanner);
                Assert.NotNull(linearWhiteSpace);
                Assert.Equal(input, linearWhiteSpace.Data);
            }
        }
    }
}