using System.IO;
using Txt.Core;
using Xunit;

namespace Txt.ABNF.Core.OCTET
{
    public class OctetLexerTest
    {
        [Theory]
        [InlineData(0x00, 0x00)]
        [InlineData(0x10, 0x10)]
        [InlineData(0x20, 0x20)]
        [InlineData(0x30, 0x30)]
        [InlineData(0x40, 0x40)]
        [InlineData(0x50, 0x50)]
        [InlineData(0x60, 0x60)]
        [InlineData(0x70, 0x70)]
        [InlineData(0x80, 0x80)]
        [InlineData(0x90, 0x90)]
        [InlineData(0x10, 0x10)]
        [InlineData(0xA0, 0xA0)]
        [InlineData(0xB0, 0xB0)]
        [InlineData(0xC0, 0xC0)]
        [InlineData(0xD0, 0xD0)]
        [InlineData(0xE0, 0xE0)]
        [InlineData(0xFF, 0xFF)]
        public void ReadSuccess(byte input, byte expected)
        {
            var octetLexer = OctetLexerFactory.Default.Create();
            using (var stream = new MemoryStream(new[] { input }))
            using (var scanner = new TextScanner(new StreamTextSource(stream)))
            {
                var result = octetLexer.Read(scanner);
                Assert.Equal(expected, result.Value);
            }
        }

        public void CannotReadPureText()
        {
            // Pretend that we are reading text from a piece of paper instead of from computer memory
            // I call this pure text: the text source is not binary encoded
            // The OCTET rule should not be used with pure text sources, because that just makes no sense
            // For that reason, I changed the OCTET lexer to read nothing unless the text source implements IBinaryDataSource
            var octetLexer = OctetLexerFactory.Default.Create();
            using (var scanner = new TextScanner(new StringTextSource("System.String")))
            {
                var result = octetLexer.Read(scanner);
                Assert.Null(result);
            }
        }
    }
}
