using Moq;
using Xunit;

namespace Txt.ABNF.Core.CTL
{
    public class ControlCharacterLexerFactoryTests
    {
        [Fact]
        public void CanReplaceAlternationLexerFactory()
        {
            var mock = new Mock<IAlternationLexerFactory>().Object;
            var sut = ControlCharacterLexerFactory.Default.UseAlternationLexerFactory(mock);
            Assert.Same(mock, sut.AlternationLexerFactory);
        }

        [Fact]
        public void CanReplaceTerminalLexerFactory()
        {
            var mock = new Mock<ITerminalLexerFactory>().Object;
            var sut = ControlCharacterLexerFactory.Default.UseTerminalLexerFactory(mock);
            Assert.Same(mock, sut.TerminalLexerFactory);
        }

        [Fact]
        public void CanReplaceValueRangeLexerFactory()
        {
            var mock = new Mock<IValueRangeLexerFactory>().Object;
            var sut = ControlCharacterLexerFactory.Default.UseValueRangeLexerFactory(mock);
            Assert.Same(mock, sut.ValueRangeLexerFactory);
        }

        [Fact]
        public void UsesDefaultAlternationLexerFactory()
        {
            var sut = ControlCharacterLexerFactory.Default;
            Assert.Same(AlternationLexerFactory.Default, sut.AlternationLexerFactory);
        }

        [Fact]
        public void UsesDefaultTerminalLexerFactory()
        {
            var sut = ControlCharacterLexerFactory.Default;
            Assert.Same(TerminalLexerFactory.Default, sut.TerminalLexerFactory);
        }

        [Fact]
        public void UsesDefaultValueRangeLexerFactory()
        {
            var sut = ControlCharacterLexerFactory.Default;
            Assert.Same(ValueRangeLexerFactory.Default, sut.ValueRangeLexerFactory);
        }
    }
}
