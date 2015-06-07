namespace SLANG
{
    using System.IO;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using SLANG.Core;

    [TestClass]
    public class LinearWhiteSpaceLexerTests
    {
        [TestMethod]
        public void ReadSp()
        {
            var text = " ";
            using (var inputStream = text.AsStream())
            using (var pushbackInputStream = new PushbackInputStream(inputStream))
            using (ITextScanner scanner = new TextScanner(pushbackInputStream))
            {
                scanner.Read();
                var lexer = new LinearWhiteSpaceLexer();
                var element = lexer.Read(scanner);
                Assert.IsNotNull(element);
                Assert.AreEqual(text, element.Data);
            }
        }

        [TestMethod]
        public void Read5Sp()
        {
            var text = "     ";
            using (var inputStream = text.AsStream())
            using (var pushbackInputStream = new PushbackInputStream(inputStream))
            using (ITextScanner scanner = new TextScanner(pushbackInputStream))
            {
                scanner.Read();
                var lexer = new LinearWhiteSpaceLexer();
                var element = lexer.Read(scanner);
                Assert.IsNotNull(element);
                Assert.AreEqual(text, element.Data);
            }
        }

        [TestMethod]
        public void ReadCrLfSp()
        {
            var text = "\r\n ";
            using (var inputStream = text.AsStream())
            using (var pushbackInputStream = new PushbackInputStream(inputStream))
            using (ITextScanner scanner = new TextScanner(pushbackInputStream))
            {
                scanner.Read();
                var lexer = new LinearWhiteSpaceLexer();
                var element = lexer.Read(scanner);
                Assert.IsNotNull(element);
                Assert.AreEqual(text, element.Data);
            }
        }

        [TestMethod]
        public void ReadEmptyString()
        {
            var text = string.Empty;
            using (var inputStream = text.AsStream())
            using (var pushbackInputStream = new PushbackInputStream(inputStream))
            using (ITextScanner scanner = new TextScanner(pushbackInputStream))
            {
                scanner.Read();
                var lexer = new LinearWhiteSpaceLexer();
                var element = lexer.Read(scanner);
                Assert.IsNotNull(element);
                Assert.IsFalse(element.Elements.Any());
                Assert.AreEqual(text, element.Data);
            }
        }

        [TestMethod]
        public void IgnoreLastCrLf()
        {
            var text = "\r\n";
            using (var inputStream = text.AsStream())
            using (var pushbackInputStream = new PushbackInputStream(inputStream))
            using (ITextScanner scanner = new TextScanner(pushbackInputStream))
            {
                scanner.Read();
                var lexer = new LinearWhiteSpaceLexer();
                var element = lexer.Read(scanner);
                Assert.IsFalse(element.Elements.Any());
            }
        }

        [TestMethod]
        public void IgnoreTryLastCrLf()
        {
            var text = "\r\n";
            using (var inputStream = text.AsStream())
            using (var pushbackInputStream = new PushbackInputStream(inputStream))
            using (ITextScanner scanner = new TextScanner(pushbackInputStream))
            {
                scanner.Read();
                var lexer = new LinearWhiteSpaceLexer();
                LinearWhiteSpace element;
                if (!lexer.TryRead(scanner, out element))
                {
                    Assert.Fail();
                }

                Assert.IsNotNull(element);
                Assert.IsFalse(element.Elements.Any());
            }
        }
    }
}
