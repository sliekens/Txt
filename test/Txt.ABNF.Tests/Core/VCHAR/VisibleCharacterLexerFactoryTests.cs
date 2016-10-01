using Moq;
using Xunit;

namespace Txt.ABNF.Core.VCHAR
{
    public class VisibleCharacterLexerFactoryTests
    {
        [Fact]
        public void CanReplaceValueRangeLexerFactory()
        {
            var mock = new Mock<IValueRangeLexerFactory>().Object;
            var sut = VisibleCharacterLexerFactory.Default.UseValueRangeLexerFactory(mock);
            Assert.Same(mock, sut.ValueRangeLexerFactory);
        }

        [Fact]
        public void UsesDefaultValueRangeLexerFactory()
        {
            var sut = VisibleCharacterLexerFactory.Default;
            Assert.Same(ValueRangeLexerFactory.Default, sut.ValueRangeLexerFactory);
        }
    }
}
