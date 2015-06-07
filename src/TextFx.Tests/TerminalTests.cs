namespace TextFx
{
    using Xunit;

    public class TerminalTests
    {
        [Theory]
        [InlineData('\r', 2, "1101")]
        [InlineData('\r', 10, "13")]
        [InlineData('\r', 16, "D")]
        public void ToBase_ShouldSucceed(char terminal, int toBase, string expected)
        {
            var value = new Terminal(terminal, new TextContext(0));
            var result = value.ToBase(toBase);
            Assert.Equal(expected, result);
        }
    }
}
