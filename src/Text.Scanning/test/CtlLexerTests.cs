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
                var token = lexer.Read(scanner);
                Assert.AreEqual(text, token.Data);
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
                CtlToken token;
                if (lexer.TryRead(scanner, out token))
                {
                    Assert.Fail();
                }

                Assert.IsNull(token);
            }
        }
    }
}
