using Txt.ABNF.Core.CRLF;
using Txt.ABNF.Core.WSP;
using Xunit;

namespace Txt.ABNF.Core.LWSP
{
    public class LinearWhiteSpaceLexerFactoryTests
    {
        [Fact]
        public void UsesDefaultAlternationLexerFactory()
        {
            var sut = LinearWhiteSpaceLexerFactory.Default;
            Assert.Same(AlternationLexerFactory.Default, sut.AlternationLexerFactory);
        }

        [Fact]
        public void UsesDefaultConcatenationLexerFactory()
        {
            var sut = LinearWhiteSpaceLexerFactory.Default;
            Assert.Same(ConcatenationLexerFactory.Default, sut.ConcatenationLexerFactory);
        }

        [Fact]
        public void UsesDefaultNewLineLexerFactory()
        {
            var sut = LinearWhiteSpaceLexerFactory.Default;
            Assert.Same(NewLineLexerFactory.Default, sut.NewLineLexerFactory);
        }

        [Fact]
        public void UsesDefaultRepetitionLexerFactory()
        {
            var sut = LinearWhiteSpaceLexerFactory.Default;
            Assert.Same(RepetitionLexerFactory.Default, sut.RepetitionLexerFactory);
        }

        [Fact]
        public void UsesDefaultWhiteSpaceLexerFactory()
        {
            var sut = LinearWhiteSpaceLexerFactory.Default;
            Assert.Same(WhiteSpaceLexerFactory.Default, sut.WhiteSpaceLexerFactory);
        }
    }
}
