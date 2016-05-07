using System;
using Xunit;

namespace Txt.Core
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