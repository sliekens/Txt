namespace SLANG.Core
{
    using Xunit;

    public class BitLexerTests
    {
        private readonly BitLexer lexer = new BitLexer(new BitAlternativeLexer(new ZeroTerminalLexer(), new OneTerminalLexer()));
        
        [Fact]
        public void CanReadZero()
        {
            var input = "0";
            using (ITextScanner textScanner = new TextScanner(new PushbackInputStream(input.ToMemoryStream())))
            {
                Assert.True(textScanner.Read());
                var bit = this.lexer.Read(textScanner);
                Assert.Equal(input, bit.Data);
            }
        }

        [Fact]
        public void CanReadOne()
        {
            var input = "1";
            using (ITextScanner textScanner = new TextScanner(new PushbackInputStream(input.ToMemoryStream())))
            {
                Assert.True(textScanner.Read());
                var bit = this.lexer.Read(textScanner);
                Assert.Equal(input, bit.Data);
            }
        }

        [Fact]
        public void CannotReadNegativeOne()
        {
            var input = "-1";
            using (ITextScanner textScanner = new TextScanner(new PushbackInputStream(input.ToMemoryStream())))
            {
                Assert.True(textScanner.Read());
                Assert.Throws<SyntaxErrorException>(() => this.lexer.Read(textScanner));
            }
        }
    }
}
