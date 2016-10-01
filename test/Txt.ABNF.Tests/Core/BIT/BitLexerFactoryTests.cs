using Moq;
using Xunit;

namespace Txt.ABNF.Core.BIT
{
    public class BitLexerFactoryTests
    {
        [Fact]
        public void CanReplaceAlternationLexerFactory()
        {
            var mock = new Mock<IAlternationLexerFactory>().Object;
            var sut = BitLexerFactory.Default.UseAlternationLexerFactory(mock);
            Assert.Same(mock, sut.AlternationLexerFactory);
        }

        [Fact]
        public void CanReplaceTerminalLexerFactory()
        {
            var mock = new Mock<ITerminalLexerFactory>().Object;
            var sut = BitLexerFactory.Default.UseTerminalLexerFactory(mock);
            Assert.Same(mock, sut.TerminalLexerFactory);
        }

        [Fact]
        public void UsesDefaultAlternationLexerFactory()
        {
            var sut = BitLexerFactory.Default;
            Assert.Same(AlternationLexerFactory.Default, sut.AlternationLexerFactory);
        }

        [Fact]
        public void UsesDefaultTerminalLexerFactory()
        {
            var sut = BitLexerFactory.Default;
            Assert.Same(TerminalLexerFactory.Default, sut.TerminalLexerFactory);
        }
    }
}
