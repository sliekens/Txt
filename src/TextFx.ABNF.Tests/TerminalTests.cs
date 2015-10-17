namespace TextFx.ABNF
{
    using Xunit;

    public class TerminalTests
    {
        [Theory]
        [InlineData("\r", 2, "%b1101")]
        [InlineData("\r", 10, "%d13")]
        [InlineData("\r", 16, "%xD")]
        public void ToBase_ShouldSucceed(string terminal, int toBase, string expected)
        {
            var value = new Terminal(terminal, new TextContext(0));
            var result = value.ToBase(toBase);
            Assert.Equal(expected, result);
        }
    }
}
