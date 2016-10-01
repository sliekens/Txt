using Moq;
using Xunit;

namespace Txt.ABNF.Core.ALPHA
{
    public class AlphaLexerFactoryTests
    {
        [Fact]
        public void CanReplaceAlternationLexerFactory()
        {
            var mock = new Mock<IAlternationLexerFactory>().Object;
            var sut = AlphaLexerFactory.Default.UseAlternationLexerFactory(mock);
            Assert.Same(mock, sut.AlternationLexerFactory);
        }

        [Fact]
        public void CanReplaceValueRangeLexerFactory()
        {
            var mock = new Mock<IValueRangeLexerFactory>().Object;
            var sut = AlphaLexerFactory.Default.UseValueRangeFactory(mock);
            Assert.Same(mock, sut.ValueRangeLexerFactory);
        }

        [Fact]
        public void UsesDefaultAlternationLexerFactory()
        {
            var sut = AlphaLexerFactory.Default;
            Assert.Same(AlternationLexerFactory.Default, sut.AlternationLexerFactory);
        }

        [Fact]
        public void UsesDefaultValueRangeLexerFactory()
        {
            var sut = AlphaLexerFactory.Default;
            Assert.Same(ValueRangeLexerFactory.Default, sut.ValueRangeLexerFactory);
        }
    }
}
