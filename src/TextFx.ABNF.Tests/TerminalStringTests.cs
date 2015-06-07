namespace TextFx.ABNF
{
    using System.Linq;

    using Xunit;

    public class TerminalStringTests
    {
        [Theory]
        [InlineData(new[] { '\r', '\n' }, 2, "%b1101.1010")]
        [InlineData(new[] { '\r', '\n' }, 10, "%d13.10")]
        [InlineData(new[] { '\r', '\n' }, 16, "%xD.A")]
        public void ToBase_ShouldSucceed(char[] terminals, int toBase, string expected)
        {
            var values = terminals.Select((c, i) => new Terminal(c, new TextContext(i))).ToList();
            var value = new TerminalString(values, new TextContext(0));
            var result = value.ToBase(toBase);
            Assert.Equal(expected, result);
        }
    }
}
