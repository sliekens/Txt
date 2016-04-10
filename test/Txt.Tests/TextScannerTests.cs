using System;
using Txt;
using Xunit;

namespace Text
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