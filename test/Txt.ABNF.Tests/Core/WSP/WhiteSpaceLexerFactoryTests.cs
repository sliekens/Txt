using Moq;
using Txt.ABNF.Core.HTAB;
using Txt.ABNF.Core.SP;
using Txt.Core;
using Xunit;

namespace Txt.ABNF.Core.WSP
{
    public class WhiteSpaceLexerFactoryTests
    {
        [Fact]
        public void CanReplaceAlternationLexerFactory()
        {
            var mock = new Mock<IAlternationLexerFactory>().Object;
            var sut = WhiteSpaceLexerFactory.Default.UseAlternationLexerFactory(mock);
            Assert.Same(mock, sut.AlternationLexerFactory);
        }

        [Fact]
        public void CanReplaceHorizontalTabLexerFactory()
        {
            var mock = new Mock<ILexerFactory<HorizontalTab>>().Object;
            var sut = WhiteSpaceLexerFactory.Default.UseHorizontalTabLexerFactory(mock);
            Assert.Same(mock, sut.HorizontalTabLexerFactory);
        }

        [Fact]
        public void CanReplaceSpaceLexerFactory()
        {
            var mock = new Mock<ILexerFactory<Space>>().Object;
            var sut = WhiteSpaceLexerFactory.Default.UseSpaceLexerFactory(mock);
            Assert.Same(mock, sut.SpaceLexerFactory);
        }

        [Fact]
        public void UsesDefaultAlternationLexerFactory()
        {
            var sut = WhiteSpaceLexerFactory.Default;
            Assert.Same(AlternationLexerFactory.Default, sut.AlternationLexerFactory);
        }

        [Fact]
        public void UsesDefaultHorizontalTabLexerFactory()
        {
            var sut = WhiteSpaceLexerFactory.Default;
            Assert.Same(HorizontalTabLexerFactory.Default, sut.HorizontalTabLexerFactory);
        }

        [Fact]
        public void UsesDefaultSpaceLexerFactory()
        {
            var sut = WhiteSpaceLexerFactory.Default;
            Assert.Same(SpaceLexerFactory.Default, sut.SpaceLexerFactory);
        }
    }
}
