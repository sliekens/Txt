using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Text.Scanning;
using Text.Scanning.Core;

namespace Text
{
    [TestClass]
    public class CtlLexerTests
    {
        [TestMethod]
        public void ReadCtl()
        {
            var text = "\u0001";
            var lexer = new CtlLexer();
            using (var reader = new StringReader(text))
            using (ITextScanner scanner = new TextScanner(reader))
            {
                scanner.Read();
                var element = lexer.Read(scanner);
                Assert.AreEqual(text, element.Data);
            }
        }

        [TestMethod]
        public void FailAlpha()
        {
            var text = "A";
            var lexer = new CtlLexer();
            using (var reader = new StringReader(text))
            using (ITextScanner scanner = new TextScanner(reader))
            {
                scanner.Read();
                CtlElement element;
                if (lexer.TryRead(scanner, out element))
                {
                    Assert.Fail();
                }

                Assert.IsNull(element);
            }
        }
    }
}
