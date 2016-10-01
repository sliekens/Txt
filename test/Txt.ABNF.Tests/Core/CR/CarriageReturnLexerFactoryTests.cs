using Moq;
using Xunit;

namespace Txt.ABNF.Core.CR
{
    public class CarriageReturnLexerFactoryTests
    {
        [Fact]
        public void CanReplaceTerminalLexerFactory()
        {
            var mock = new Mock<ITerminalLexerFactory>().Object;
            var sut = CarriageReturnLexerFactory.Default.UseTerminalLexerFactory(mock);
            Assert.Same(mock, sut.TerminalLexerFactory);
        }

        [Fact]
        public void UsesDefaultTerminalLexerFactory()
        {
            var sut = CarriageReturnLexerFactory.Default;
            Assert.Same(TerminalLexerFactory.Default, sut.TerminalLexerFactory);
        }
    }
}
