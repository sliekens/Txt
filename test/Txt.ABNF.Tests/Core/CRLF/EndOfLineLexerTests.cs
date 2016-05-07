using Txt.Core;
using Txt.ABNF.Core.CR;
using Txt.ABNF.Core.LF;
using Xunit;

namespace Txt.ABNF.Core.CRLF
{
    public class EndOfLineLexerTests
    {
        [Theory]
        [InlineData("\r\n")]
        public void ReadSuccess(string input)
        {
            var terminalsLexerFactory = new TerminalLexerFactory();
            var sequenceLexerFactory = new ConcatenationLexerFactory();
            var carriageReturnLexerFactory = new CarriageReturnLexerFactory(terminalsLexerFactory);
            var lineFeedLexerFactory = new LineFeedLexerFactory(terminalsLexerFactory);
            var factory = new NewLineLexerFactory(sequenceLexerFactory, carriageReturnLexerFactory.Create(), lineFeedLexerFactory.Create());
            var newLineLexer = factory.Create();
            using (var scanner = new TextScanner(new StringTextSource(input)))
            {
                var result = newLineLexer.Read(scanner);
                Assert.NotNull(result);
                Assert.True(result.Success);
                Assert.NotNull(result.Element);
                Assert.Equal(input, result.Element.Text);
            }
        }
    }
}
