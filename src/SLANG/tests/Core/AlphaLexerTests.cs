namespace SLANG.Core
{
    using Microsoft.Practices.ServiceLocation;

    using Xunit;

    public partial class AlphaLexerTests
    {
    }

    public partial class AlphaLexerTests
    {
        public class UppercaseLexerTests
        {
            private readonly AlphaLexer.UpperCaseLexer lexer = new AlphaLexer.UpperCaseLexer(ServiceLocator.Current);

            static UppercaseLexerTests()
            {
                ServiceLocator.SetLocatorProvider(() => new FakeServiceLocator());
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
                using (ITextScanner textScanner = new TextScanner(new PushbackInputStream(letter.ToMemoryStream())))
                {
                    textScanner.Read();
                    var alpha = this.lexer.Read(textScanner);
                    Assert.Equal(letter, alpha.Data);
                }
            }
        }
    }

    public partial class AlphaLexerTests
    {
        public class LowercaseLexerTests
        {
            private readonly AlphaLexer.LowerCaseLexer lexer = new AlphaLexer.LowerCaseLexer(ServiceLocator.Current);

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
                using (ITextScanner textScanner = new TextScanner(new PushbackInputStream(letter.ToMemoryStream())))
                {
                    textScanner.Read();
                    var alpha = this.lexer.Read(textScanner);
                    Assert.Equal(letter, alpha.Data);
                }
            }
        }
    }
}
