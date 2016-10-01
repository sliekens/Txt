using Xunit;

namespace Txt.ABNF.Core.LF
{
    public class LineFeedLexerFactoryTests
    {
        [Fact]
        public void CanReplaceTerminalLexerFactory()
        {
            var terminalLexerFactory = new TerminalLexerFactory();
            var sut = LineFeedLexerFactory.Default.UseTerminalLexerFactory(terminalLexerFactory);
            Assert.Same(terminalLexerFactory, sut.TerminalLexerFactory);
        }

        [Fact]
        public void UsesDefaultTerminalLexerFactory()
        {
            var sut = LineFeedLexerFactory.Default;
            Assert.Same(TerminalLexerFactory.Default, sut.TerminalLexerFactory);
        }
    }
}
