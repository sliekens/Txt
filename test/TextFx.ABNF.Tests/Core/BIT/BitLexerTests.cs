namespace TextFx.ABNF.Core
{
    using Xunit;

    public class BitLexerTests
    {
        [Fact]
        public void CanReadZero()
        {
            var input = "0";
            var factory = new BitLexerFactory(new AlternativeLexerFactory(), new TerminalLexerFactory());
            var bitLexer = factory.Create();
            using (var scanner = new TextScanner(new StringTextSource(input)))
            {
                var result = bitLexer.Read(scanner);
                Assert.NotNull(result);
                Assert.True(result.Success);
                Assert.NotNull(result.Element);
                Assert.Equal(input, result.Element.Text);
            }
        }

        [Fact]
        public void CanReadOne()
        {
            var input = "1";
            var factory = new BitLexerFactory(new AlternativeLexerFactory(), new TerminalLexerFactory());
            var bitLexer = factory.Create();
            using (var scanner = new TextScanner(new StringTextSource(input)))
            {
                var result = bitLexer.Read(scanner);
                Assert.NotNull(result);
                Assert.True(result.Success);
                Assert.NotNull(result.Element);
                Assert.Equal(input, result.Element.Text);
            }
        }

        [Fact]
        public void CannotReadNegativeOne()
        {
            var input = "-1";
            var factory = new BitLexerFactory(new AlternativeLexerFactory(), new TerminalLexerFactory());
            var bitLexer = factory.Create();
            using (var scanner = new TextScanner(new StringTextSource(input)))
            {
                var result = bitLexer.Read(scanner);
                Assert.NotNull(result);
                Assert.False(result.Success);
                Assert.Null(result.Element);
            }
        }
    }
}
