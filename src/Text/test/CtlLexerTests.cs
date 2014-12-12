using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Text.Core;

namespace Text
{
    [TestClass]
    public class CtlLexerTests
    {
        [TestMethod]
        public void ReadCtl()
        {
            var text = "\u0001";
            using (var reader = new StringReader(text))
            using (var scanner = new TextScanner(reader))
            {
                scanner.Read();
                var lexer = new CtlLexer(scanner);
                var token = lexer.Read();
                Assert.AreEqual(text, token.Data);
            }
        }

        [TestMethod]
        public void FailNul()
        {
            var text = "\0";
            using (var reader = new StringReader(text))
            using (var scanner = new TextScanner(reader))
            {
                scanner.Read();
                var lexer = new CtlLexer(scanner);
                CtlToken token;
                if (lexer.TryRead(out token))
                {
                    Assert.Fail();
                }

                Assert.IsNull(token);
            }
        }
    }
}
