using System.Text;
using Txt.Core;
using Xunit;

namespace Txt.ABNF
{
    public class ValueRangeLexerFactoryTest
    {
        [Theory]
        [InlineData(0x41, 0x5A, "ascii", "A")]
        [InlineData(0x41, 0x5A, "ascii", "B")]
        [InlineData(0x41, 0x5A, "ascii", "C")]
        [InlineData(0x41, 0x5A, "utf-8", "A")]
        [InlineData(0x41, 0x5A, "utf-8", "B")]
        [InlineData(0x41, 0x5A, "utf-8", "C")]
        [InlineData(0x41, 0x5A, "utf-16", "A")]
        [InlineData(0x41, 0x5A, "utf-16", "B")]
        [InlineData(0x41, 0x5A, "utf-16", "C")]
        public void FromRange(int lowerBound, int upperBound, string encoding, string s)
        {
            var enc = Encoding.GetEncoding(encoding);
            var sut = new ValueRangeLexerFactory();
            var lexer = sut.Create(lowerBound, upperBound, enc);
            using (var text = new StringTextSource(s))
            using (var scanner = new TextScanner(text))
            {
                var result = lexer.Read(scanner);
                Assert.Equal(s, result.Text);
            }
        }
    }
}
