using Txt.Core;
using Xunit;

namespace Txt.ABNF.Core.BIT
{
    public class BitLexerTests
    {
        [Fact]
        public void CanReadZero()
        {
            var input = "0";
            var factory = new BitLexerFactory(new AlternationLexerFactory(), new TerminalLexerFactory());
            var bitLexer = factory.Create();
            using (var scanner = new TextScanner(new StringTextSource(input)))
            {
                var result = bitLexer.Read(scanner);
                Assert.Equal(input, result.Text);
            }
        }

        [Fact]
        public void CanReadOne()
        {
            var input = "1";
            var factory = new BitLexerFactory(new AlternationLexerFactory(), new TerminalLexerFactory());
            var bitLexer = factory.Create();
            using (var scanner = new TextScanner(new StringTextSource(input)))
            {
                var result = bitLexer.Read(scanner);
                Assert.Equal(input, result.Text);
            }
        }

        [Fact]
        public void CannotReadNegativeOne()
        {
            var input = "-1";
            var factory = new BitLexerFactory(new AlternationLexerFactory(), new TerminalLexerFactory());
            var bitLexer = factory.Create();
            using (var scanner = new TextScanner(new StringTextSource(input)))
            {
                var result = bitLexer.Read(scanner);
                Assert.Null(result);
            }
        }
    }
}
