namespace SLANG
{
    using System.IO;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using SLANG.Core;

    [TestClass]
    public class EndOfLineLexerTests
    {
        [TestMethod]
        public void ReadCrLf()
        {
            var text = "\r\n";
            using (var inputStream = text.AsStream())
            using (var pushbackInputStream = new PushbackInputStream(inputStream))
            using (ITextScanner scanner = new TextScanner(pushbackInputStream))
            {
                scanner.Read();
                var lexer = new EndOfLineLexer();
                var element = lexer.Read(scanner);
                Assert.IsNotNull(element);
                Assert.AreEqual("\r\n", element.Data);
            }
        }

        [TestMethod]
        public void FailCr()
        {
            var text = "\r";
            using (var inputStream = text.AsStream())
            using (var pushbackInputStream = new PushbackInputStream(inputStream))
            using (ITextScanner scanner = new TextScanner(pushbackInputStream))
            {
                scanner.Read();
                var lexer = new EndOfLineLexer();
                EndOfLine element;
                if (lexer.TryRead(scanner, out element))
                {
                    Assert.Fail();
                }

                Assert.IsNull(element);
            }
        }

        [TestMethod]
        public void FailLf()
        {
            var text = "\n";
            using (var inputStream = text.AsStream())
            using (var pushbackInputStream = new PushbackInputStream(inputStream))
            using (ITextScanner scanner = new TextScanner(pushbackInputStream))
            {
                scanner.Read();
                var lexer = new EndOfLineLexer();
                EndOfLine element;
                if (lexer.TryRead(scanner, out element))
                {
                    Assert.Fail();
                }

                Assert.IsNull(element);
            }
        }
    }
}
