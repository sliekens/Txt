using Moq;
using Txt.ABNF.Core.DIGIT;
using Txt.Core;
using Xunit;

namespace Txt.ABNF.Core.HEXDIG
{
    public class HexadecimalDigitLexerFactoryTests
    {
        [Fact]
        public void CanReplaceAlternationLexerFactory()
        {
            var mock = new Mock<IAlternationLexerFactory>().Object;
            var sut = HexadecimalDigitLexerFactory.Default.UseAlternationLexerFactory(mock);
            Assert.Same(mock, sut.AlternationLexerFactory);
        }

        [Fact]
        public void CanReplaceDigitLexerFactory()
        {
            var mock = new Mock<ILexerFactory<Digit>>().Object;
            var sut = HexadecimalDigitLexerFactory.Default.UseDigitLexerFactory(mock);
            Assert.Same(mock, sut.DigitLexerFactory);
        }

        [Fact]
        public void CanReplaceTerminalLexerFactory()
        {
            var mock = new Mock<ITerminalLexerFactory>().Object;
            var sut = HexadecimalDigitLexerFactory.Default.UseTerminalLexerFactory(mock);
            Assert.Same(mock, sut.TerminalLexerFactory);
        }

        [Fact]
        public void UsesDefaultAlternationLexerFactory()
        {
            var sut = HexadecimalDigitLexerFactory.Default;
            Assert.Same(AlternationLexerFactory.Default, sut.AlternationLexerFactory);
        }

        [Fact]
        public void UsesDefaultDigitLexerFactory()
        {
            var sut = HexadecimalDigitLexerFactory.Default;
            Assert.Same(DigitLexerFactory.Default, sut.DigitLexerFactory);
        }

        [Fact]
        public void UsesDefaultTerminalLexerFactory()
        {
            var sut = HexadecimalDigitLexerFactory.Default;
            Assert.Same(TerminalLexerFactory.Default, sut.TerminalLexerFactory);
        }
    }
}
