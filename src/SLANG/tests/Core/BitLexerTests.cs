namespace SLANG.Core
{
    using Microsoft.Practices.ServiceLocation;

    using Xunit;

    public partial class BitLexerTests
    {
    }

    public partial class BitLexerTests
    {
        public class ZeroLexerTests
        {
            private readonly BitLexer.ZeroLexer lexer = new BitLexer.ZeroLexer(ServiceLocator.Current);

            static ZeroLexerTests()
            {
                ServiceLocator.SetLocatorProvider(() => new FakeServiceLocator());
            }

            [Fact]
            public void CanReadZero()
            {
                var input = "0";
                using (ITextScanner textScanner = new TextScanner(new PushbackInputStream(input.ToMemoryStream())))
                {
                    Assert.True(textScanner.Read());
                    var bit = this.lexer.Read(textScanner);
                    Assert.Equal(input, bit.Data);
                }
            }
        }
    }

    public partial class BitLexerTests
    {
        public class OneLexerTests
        {
            private readonly BitLexer.OneLexer lexer = new BitLexer.OneLexer(ServiceLocator.Current);

            static OneLexerTests()
            {
                ServiceLocator.SetLocatorProvider(() => new FakeServiceLocator());
            }

            [Fact]
            public void CanReadOne()
            {
                var input = "1";
                using (ITextScanner textScanner = new TextScanner(new PushbackInputStream(input.ToMemoryStream())))
                {
                    Assert.True(textScanner.Read());
                    var bit = this.lexer.Read(textScanner);
                    Assert.Equal(input, bit.Data);
                }
            }
        }
    }
}
