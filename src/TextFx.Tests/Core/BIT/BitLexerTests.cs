namespace TextFx.Core
{
    using System;

    using Xunit;

    public class BitLexerTests
    {
        [Fact]
        public void CanReadZero()
        {
            var input = "0";
            var factory = new BitLexerFactory(new AlternativeLexerFactory(), new StringLexerFactory(new CaseInsensitiveTerminalLexerFactory()));
            var bitLexer = factory.Create();
            using (var scanner = new TextScanner(new PushbackInputStream(input.ToMemoryStream())))
            {
                scanner.Read();
                var bit = bitLexer.Read(scanner);
                Assert.Equal(input, bit.Values);
            }
        }

        [Fact]
        public void CanReadOne()
        {
            var input = "1";
            var factory = new BitLexerFactory(new AlternativeLexerFactory(), new StringLexerFactory(new CaseInsensitiveTerminalLexerFactory()));
            var bitLexer = factory.Create();
            using (var scanner = new TextScanner(new PushbackInputStream(input.ToMemoryStream())))
            {
                scanner.Read();
                var bit = bitLexer.Read(scanner);
                Assert.Equal(input, bit.Values);
            }
        }

        [Fact]
        public void CannotReadNegativeOne()
        {
            var input = "-1";
            var factory = new BitLexerFactory(new AlternativeLexerFactory(), new StringLexerFactory(new CaseInsensitiveTerminalLexerFactory()));
            var bitLexer = factory.Create();
            using (var scanner = new TextScanner(new PushbackInputStream(input.ToMemoryStream())))
            {
                scanner.Read();
                Assert.Throws<FormatException>(() => bitLexer.Read(scanner));
            }
        }
    }
}
