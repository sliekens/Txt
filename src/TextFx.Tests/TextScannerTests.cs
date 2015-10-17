using System;
using Xunit;

namespace TextFx.Tests
{
    public class TextScannerTests
    {
        [Fact]
        public void Ctor_ThrowsOnNullAsTextSource()
        {
            Assert.Throws<ArgumentNullException>(() => new TextScanner(null));
        }

        [Fact]
        public void Ctor_DoesNotInitializeReader()
        {
            var stub = new FakeTextSource();
            var sut = new TextScanner(stub);
            Assert.Equal(-1, sut.Offset);
        }

        [Fact]
        public void TryMatch_ThrowsWhenNotInitialized()
        {
            var stub = new FakeTextSource();
            var sut = new TextScanner(stub);
            char next;
            Assert.Throws<InvalidOperationException>(() => sut.TryMatch('x', out next));
        }

        [Fact]
        public void TryMatchIgnoreCase_ThrowsWhenNotInitialized()
        {
            var stub = new FakeTextSource();
            var sut = new TextScanner(stub);
            char next;
            Assert.Throws<InvalidOperationException>(() => sut.TryMatchIgnoreCase('x', out next));
        }

        [Fact]
        public void GetContext_ThrowsWhenNotInitialized()
        {
            var stub = new FakeTextSource();
            var sut = new TextScanner(stub);
            Assert.Throws<InvalidOperationException>(() => sut.GetContext());
        }
    }
}