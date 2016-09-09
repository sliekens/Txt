using Txt.Core;
using Txt.ABNF.Core.DIGIT;
using Xunit;

namespace Txt.ABNF.Core.HEXDIG
{
    public class HexadecimalDigitLexerTests
    {
        [Theory]
        [InlineData("0")]
        [InlineData("1")]
        [InlineData("2")]
        [InlineData("3")]
        [InlineData("4")]
        [InlineData("5")]
        [InlineData("6")]
        [InlineData("7")]
        [InlineData("8")]
        [InlineData("9")]
        [InlineData("A")]
        [InlineData("B")]
        [InlineData("C")]
        [InlineData("D")]
        [InlineData("E")]
        [InlineData("F")]
        [InlineData("a")]
        [InlineData("b")]
        [InlineData("c")]
        [InlineData("d")]
        [InlineData("e")]
        [InlineData("f")]
        public void ReadSuccess(string input)
        {
            var valueRangeLexerFactory = new ValueRangeLexerFactory();
            var digitLexerFactory = new DigitLexerFactory(valueRangeLexerFactory);
            var terminalLexerFactory = new TerminalLexerFactory();
            var alternativeLexerFactory = new AlternationLexerFactory();
            var factory = new HexadecimalDigitLexerFactory(alternativeLexerFactory, terminalLexerFactory, digitLexerFactory.Create());
            var lexer = factory.Create();
            using (var scanner = new TextScanner(new StringTextSource(input)))
            {
                var result = lexer.Read(scanner);
                Assert.Equal(input, result.Text);
            }
        }
    }
}
