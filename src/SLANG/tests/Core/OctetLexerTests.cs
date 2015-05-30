namespace SLANG.Core
{
    using Xunit;

    public class OctetLexerTests
    {
        [Theory]
        [InlineData("\x00")]
        [InlineData("\x10")]
        [InlineData("\x20")]
        [InlineData("\x30")]
        [InlineData("\x40")]
        [InlineData("\x50")]
        [InlineData("\x60")]
        [InlineData("\x70")]
        [InlineData("\x80")]
        [InlineData("\x90")]
        [InlineData("\x10")]
        [InlineData("\xA0")]
        [InlineData("\xB0")]
        [InlineData("\xC0")]
        [InlineData("\xD0")]
        [InlineData("\xE0")]
        [InlineData("\xFF")]
        public void ReadSuccess(string input)
        {
            var octetLexer = new OctetLexer(new OctetValueRangeLexer());
            using (var scanner = new TextScanner(new PushbackInputStream(input.ToMemoryStream())))
            {
                scanner.Read();
                var octet = octetLexer.Read(scanner);
                Assert.NotNull(octet);
                Assert.Equal(input, octet.Data);
            }
        }
    }
}
