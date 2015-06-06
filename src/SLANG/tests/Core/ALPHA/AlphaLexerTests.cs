namespace SLANG.Core
{
    using Xunit;

    public class AlphaLexerTests
    {
        [Theory]
        [InlineData("a")]
        [InlineData("b")]
        [InlineData("c")]
        [InlineData("d")]
        [InlineData("e")]
        [InlineData("f")]
        [InlineData("g")]
        [InlineData("h")]
        [InlineData("i")]
        [InlineData("j")]
        [InlineData("k")]
        [InlineData("l")]
        [InlineData("m")]
        [InlineData("n")]
        [InlineData("o")]
        [InlineData("p")]
        [InlineData("q")]
        [InlineData("r")]
        [InlineData("s")]
        [InlineData("t")]
        [InlineData("u")]
        [InlineData("v")]
        [InlineData("w")]
        [InlineData("x")]
        [InlineData("y")]
        [InlineData("z")]
        public void CanReadLowercaseAsciiLetters(string letter)
        {
            var factory = new AlphaLexerFactory(new ValueRangeLexerFactory(), new AlternativeLexerFactory());
            var alphaLexer = factory.Create();
            using (var scanner = new TextScanner(new PushbackInputStream(letter.ToMemoryStream())))
            {
                scanner.Read();
                var alpha = alphaLexer.Read(scanner);
                Assert.Equal(letter, alpha.Values);
            }
        }

        [Theory]
        [InlineData("A")]
        [InlineData("B")]
        [InlineData("C")]
        [InlineData("D")]
        [InlineData("E")]
        [InlineData("F")]
        [InlineData("G")]
        [InlineData("H")]
        [InlineData("I")]
        [InlineData("J")]
        [InlineData("K")]
        [InlineData("L")]
        [InlineData("M")]
        [InlineData("N")]
        [InlineData("O")]
        [InlineData("P")]
        [InlineData("Q")]
        [InlineData("R")]
        [InlineData("S")]
        [InlineData("T")]
        [InlineData("U")]
        [InlineData("V")]
        [InlineData("W")]
        [InlineData("X")]
        [InlineData("Y")]
        [InlineData("Z")]
        public void CanReadUppercaseAsciiLetters(string letter)
        {
            var factory = new AlphaLexerFactory(new ValueRangeLexerFactory(), new AlternativeLexerFactory());
            var alphaLexer = factory.Create();
            using (var scanner = new TextScanner(new PushbackInputStream(letter.ToMemoryStream())))
            {
                scanner.Read();
                var alpha = alphaLexer.Read(scanner);
                Assert.Equal(letter, alpha.Values);
            }
        }
    }
}
