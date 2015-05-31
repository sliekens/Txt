namespace SLANG.Core
{
    using Xunit;

    public class BitLexerTests
    {
        [Fact]
        public void CanReadZero()
        {
            var input = "0";
            var zeroStringLexer = new StringLexer("0");
            var oneStringLexer = new StringLexer("1");
            var bitAlternativeLexer = new AlternativeLexer(zeroStringLexer, oneStringLexer);
            var bitLexer = new BitLexer(bitAlternativeLexer);
            using (var scanner = new TextScanner(new PushbackInputStream(input.ToMemoryStream())))
            {
                scanner.Read();
                var bit = bitLexer.Read(scanner);
                Assert.Equal(input, bit.Data);
            }
        }

        [Fact]
        public void CanReadOne()
        {
            var input = "1";
            var zeroStringLexer = new StringLexer("0");
            var oneStringLexer = new StringLexer("1"); 
            var bitAlternativeLexer = new AlternativeLexer(zeroStringLexer, oneStringLexer);
            var bitLexer = new BitLexer(bitAlternativeLexer);
            using (var scanner = new TextScanner(new PushbackInputStream(input.ToMemoryStream())))
            {
                scanner.Read();
                var bit = bitLexer.Read(scanner);
                Assert.Equal(input, bit.Data);
            }
        }

        [Fact]
        public void CannotReadNegativeOne()
        {
            var input = "-1";
            var zeroStringLexer = new StringLexer("0");
            var oneStringLexer = new StringLexer("1");
            var bitAlternativeLexer = new AlternativeLexer(zeroStringLexer, oneStringLexer);
            var bitLexer = new BitLexer(bitAlternativeLexer);
            using (var scanner = new TextScanner(new PushbackInputStream(input.ToMemoryStream())))
            {
                scanner.Read();
                Assert.Throws<SyntaxErrorException>(() => bitLexer.Read(scanner));
            }
        }
    }
}
