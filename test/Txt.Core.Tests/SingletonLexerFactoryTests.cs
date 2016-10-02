using Moq;
using Xunit;

namespace Txt.Core
{
    public class SingletonLexerFactoryTests
    {
        [Fact]
        public void FactMethodName()
        {
            var mock = new Mock<ILexerFactory<Element>>(MockBehavior.Strict);
            mock.Setup(factory => factory.Create()).Returns(() => new Mock<ILexer<Element>>().Object);
            var sut = new SingletonLexerFactory<Element>(mock.Object);
            var first = sut.Create();
            var subsequent = sut.Create();
            mock.Verify(factory => factory.Create(), Times.Once);
            Assert.Same(first, subsequent);
        }
    }
}
