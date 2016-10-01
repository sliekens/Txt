using Moq;
using Xunit;

namespace Txt.ABNF.Core.HTAB
{
    public class HorizontalTabLexerFactoryTests
    {
        [Fact]
        public void CanReplaceTerminalLexerFactory()
        {
            var mock = new Mock<ITerminalLexerFactory>().Object;
            var sut = HorizontalTabLexerFactory.Default.UseTerminalLexerFactory(mock);
            Assert.Same(mock, sut.TerminalLexerFactory);
        }

        [Fact]
        public void UsesDefaultTerminalLexerFactory()
        {
            var sut = HorizontalTabLexerFactory.Default;
            Assert.Same(TerminalLexerFactory.Default, sut.TerminalLexerFactory);
        }
    }
}
