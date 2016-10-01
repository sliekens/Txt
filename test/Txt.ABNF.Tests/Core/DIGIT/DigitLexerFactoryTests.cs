using Moq;
using Xunit;

namespace Txt.ABNF.Core.DIGIT
{
    public class DigitLexerFactoryTests
    {
        [Fact]
        public void CanReplaceValueRangeLexerFactory()
        {
            var mock = new Mock<IValueRangeLexerFactory>().Object;
            var sut = DigitLexerFactory.Default.UseValueRangeLexerFactory(mock);
            Assert.Same(mock, sut.ValueRangeLexerFactory);
        }

        [Fact]
        public void UsesDefaultValueRangeLexerFactory()
        {
            var sut = DigitLexerFactory.Default;
            Assert.Same(ValueRangeLexerFactory.Default, sut.ValueRangeLexerFactory);
        }
    }
}
