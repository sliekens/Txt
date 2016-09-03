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
                Assert.NotNull(result);
                Assert.True(result.IsSuccess);
                Assert.NotNull(result.Element);
                Assert.Equal(input, result.Element.Text);
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
                Assert.NotNull(result);
                Assert.True(result.IsSuccess);
                Assert.NotNull(result.Element);
                Assert.Equal(input, result.Element.Text);
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
                Assert.NotNull(result);
                Assert.False(result.IsSuccess);
                Assert.Null(result.Element);
            }
        }
    }
}
