using Moq;
using Xunit;

namespace Txt.ABNF.Core.SP
{
    public class SpaceLexerFactoryTests
    {
        [Fact]
        public void CanReplaceTerminalLexerFactory()
        {
            var mock = new Mock<ITerminalLexerFactory>().Object;
            var sut = SpaceLexerFactory.Default.UseTerminalLexerFactory(mock);
            Assert.Same(mock, sut.TerminalLexerFactory);
        }

        [Fact]
        public void UsesDefaultTerminalLexerFactory()
        {
            var sut = SpaceLexerFactory.Default;
            Assert.Same(TerminalLexerFactory.Default, sut.TerminalLexerFactory);
        }
    }
}
