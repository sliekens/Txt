namespace TextFx.Core
{
    using Xunit;

    public class OctetLexerTests
    {
        [Theory]
        [InlineData("\x0000", "0x00")]
        [InlineData("\x0010", "0x10")]
        [InlineData("\x0020", "0x20")]
        [InlineData("\x0030", "0x30")]
        [InlineData("\x0040", "0x40")]
        [InlineData("\x0050", "0x50")]
        [InlineData("\x0060", "0x60")]
        [InlineData("\x0070", "0x70")]
        [InlineData("\x0080", "0x80")]
        [InlineData("\x0090", "0x90")]
        [InlineData("\x0010", "0x10")]
        [InlineData("\x00A0", "0xA0")]
        [InlineData("\x00B0", "0xB0")]
        [InlineData("\x00C0", "0xC0")]
        [InlineData("\x00D0", "0xD0")]
        [InlineData("\x00E0", "0xE0")]
        [InlineData("\x00FF", "0xFF")]
        public void ReadSuccess(string input, string displayName)
        {
            var factory = new OctetLexerFactory(new ValueRangeLexerFactory());
            var octetLexer = factory.Create();
            using (var scanner = new TextScanner(new PushbackInputStream(input.ToMemoryStream())))
            {
                scanner.Read();
                var octet = octetLexer.Read(scanner);
                Assert.NotNull(octet);
                Assert.Equal(input, octet.Values);
            }
        }
    }
}
