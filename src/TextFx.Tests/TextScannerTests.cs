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
    }
}