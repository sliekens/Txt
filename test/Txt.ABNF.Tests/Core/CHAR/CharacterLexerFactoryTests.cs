using Moq;
using Xunit;

namespace Txt.ABNF.Core.CHAR
{
    public class CharacterLexerFactoryTests
    {
        [Fact]
        public void CanReplaceValueRangeLexerFactory()
        {
            var mock = new Mock<IValueRangeLexerFactory>().Object;
            var sut = CharacterLexerFactory.Default.UseValueRangeLexerFactory(mock);
            Assert.Same(mock, sut.ValueRangeLexerFactory);
        }

        [Fact]
        public void UsesDefaultValueRangeLexerFactory()
        {
            var sut = CharacterLexerFactory.Default;
            Assert.Same(ValueRangeLexerFactory.Default, sut.ValueRangeLexerFactory);
        }
    }
}
