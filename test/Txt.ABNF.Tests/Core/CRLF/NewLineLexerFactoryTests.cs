using Moq;
using Txt.ABNF.Core.CR;
using Txt.ABNF.Core.LF;
using Txt.Core;
using Xunit;

namespace Txt.ABNF.Core.CRLF
{
    public class NewLineLexerFactoryTests
    {
        [Fact]
        public void CanReplaceCarriageReturnLexerFactory()
        {
            var mock = new Mock<ILexerFactory<CarriageReturn>>().Object;
            var sut = NewLineLexerFactory.Default.UseCarriageReturnLexerFactory(mock);
            Assert.Same(mock, sut.CarriageReturnLexerFactory);
        }

        [Fact]
        public void CanReplaceConcatenationLexerFactory()
        {
            var mock = new Mock<IConcatenationLexerFactory>().Object;
            var sut = NewLineLexerFactory.Default.UseConcatenationLexerFactory(mock);
            Assert.Same(mock, sut.ConcatenationLexerFactory);
        }

        [Fact]
        public void CanReplaceLineFeedLexerFactory()
        {
            var mock = new Mock<ILexerFactory<LineFeed>>().Object;
            var sut = NewLineLexerFactory.Default.UseLineFeedLexerFactory(mock);
            Assert.Same(mock, sut.LineFeedLexerFactory);
        }

        [Fact]
        public void UsesDefaultCarriageReturnLexerFactory()
        {
            var sut = NewLineLexerFactory.Default;
            Assert.Same(CarriageReturnLexerFactory.Default, sut.CarriageReturnLexerFactory);
        }

        [Fact]
        public void UsesDefaultConcatenationLexerFactory()
        {
            var sut = NewLineLexerFactory.Default;
            Assert.Same(ConcatenationLexerFactory.Default, sut.ConcatenationLexerFactory);
        }

        [Fact]
        public void UsesDefaultLineFeedLexerFactory()
        {
            var sut = NewLineLexerFactory.Default;
            Assert.Same(LineFeedLexerFactory.Default, sut.LineFeedLexerFactory);
        }
    }
}
