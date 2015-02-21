using System;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Text.Scanning;
using Text.Scanning.Core;

namespace Text
{
    [TestClass]
    public class LinearWhiteSpaceLexerTests
    {
        [TestMethod]
        public void ReadSp()
        {
            var text = " ";
            using (var reader = new StringReader(text))
            using (ITextScanner scanner = new TextScanner(reader))
            {
                scanner.Read();
                var lexer = new LinearWhiteSpaceLexer();
                var element = lexer.Read(scanner);
                Assert.IsNotNull(element);
                Assert.IsNotNull(element.LWsp);
                Assert.AreEqual(text, element.Data);
            }
        }

        [TestMethod]
        public void Read5Sp()
        {
            var text = "     ";
            using (var reader = new StringReader(text))
            using (ITextScanner scanner = new TextScanner(reader))
            {
                scanner.Read();
                var lexer = new LinearWhiteSpaceLexer();
                var element = lexer.Read(scanner);
                Assert.IsNotNull(element);
                Assert.IsNotNull(element.LWsp);
                Assert.AreEqual(text, element.Data);
            }
        }

        [TestMethod]
        public void ReadCrLfSp()
        {
            var text = "\r\n ";
            using (var reader = new StringReader(text))
            using (ITextScanner scanner = new TextScanner(reader))
            {
                scanner.Read();
                var lexer = new LinearWhiteSpaceLexer();
                var element = lexer.Read(scanner);
                Assert.IsNotNull(element);
                Assert.IsNotNull(element.LWsp);
                Assert.AreEqual(text, element.Data);
            }
        }

        [TestMethod]
        public void ReadEmptyString()
        {
            var text = string.Empty;
            using (var reader = new StringReader(text))
            using (ITextScanner scanner = new TextScanner(reader))
            {
                scanner.Read();
                var lexer = new LinearWhiteSpaceLexer();
                var element = lexer.Read(scanner);
                Assert.IsNotNull(element);
                Assert.IsNotNull(element.LWsp);
                Assert.IsFalse(element.LWsp.Any());
                Assert.AreEqual(text, element.Data);
            }
        }

        [TestMethod]
        public void IgnoreLastCrLf()
        {
            var text = "\r\n";
            using (var reader = new StringReader(text))
            using (ITextScanner scanner = new TextScanner(reader))
            {
                scanner.Read();
                var lexer = new LinearWhiteSpaceLexer();
                var element = lexer.Read(scanner);
                Assert.IsFalse(element.LWsp.Any());
            }
        }

        [TestMethod]
        public void IgnoreTryLastCrLf()
        {
            var text = "\r\n";
            using (var reader = new StringReader(text))
            using (ITextScanner scanner = new TextScanner(reader))
            {
                scanner.Read();
                var lexer = new LinearWhiteSpaceLexer();
                LinearWhiteSpace element;
                if (!lexer.TryRead(scanner, out element))
                {
                    Assert.Fail();
                }

                Assert.IsNotNull(element);
                Assert.IsNotNull(element.LWsp);
                Assert.IsFalse(element.LWsp.Any());
            }
        }
    }
}
