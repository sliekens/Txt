using System.Diagnostics;
using Moq;
using Xunit;

namespace Txt.Core
{
    public class SingletonLexerFactoryTests
    {
        [Fact]
        public void FactoryAlwaysReturnsSameInstance()
        {
            var fakeLexerFactory = GetFakeLexerFactory();
            var sut = new SingletonLexerFactory<Element>(fakeLexerFactory.Object);
            var first = sut.Create();
            var subsequent = sut.Create();
            Assert.Same(first, subsequent);
        }

        [Fact]
        public void UnderlyingFactoryIsInvokedOnlyOnce()
        {
            var fakeLexerFactory = GetFakeLexerFactory();
            var sut = new SingletonLexerFactory<Element>(fakeLexerFactory.Object);
            sut.Create();
            sut.Create();
            fakeLexerFactory.Verify(factory => factory.Create(), Times.Once);
        }

        private static Mock<ILexerFactory<Element>> GetFakeLexerFactory()
        {
            var fakeLexerFactory = new Mock<ILexerFactory<Element>>(MockBehavior.Strict);
            var setup = fakeLexerFactory.Setup(factory => factory.Create());
            Debug.Assert(setup != null, "setup != null");
            setup.Returns(() => new Mock<ILexer<Element>>().Object);
            return fakeLexerFactory;
        }
    }
}
