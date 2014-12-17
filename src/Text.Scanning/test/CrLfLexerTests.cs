using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Text.Scanning;
using Text.Scanning.Core;

namespace Text
{
    [TestClass]
    public class CrLfLexerTests
    {
        [TestMethod]
        public void ReadCrLf()
        {
            var text = "\r\n";
            using (var reader = new StringReader(text))
            using (ITextScanner scanner = new TextScanner(reader))
            {
                scanner.Read();
                var lexer = new CrLfLexer(scanner);
                var token = lexer.Read();
                Assert.IsNotNull(token);
                Assert.AreEqual("\r\n", token.Data);
            }
        }

        [TestMethod]
        public void FailCr()
        {
            var text = "\r";
            using (var reader = new StringReader(text))
            using (ITextScanner scanner = new TextScanner(reader))
            {
                scanner.Read();
                var lexer = new CrLfLexer(scanner);
                CrLfToken token;
                if (lexer.TryRead(out token))
                {
                    Assert.Fail();
                }

                Assert.IsNull(token);
            }
        }

        [TestMethod]
        public void FailLf()
        {
            var text = "\n";
            using (var reader = new StringReader(text))
            using (ITextScanner scanner = new TextScanner(reader))
            {
                scanner.Read();
                var lexer = new CrLfLexer(scanner);
                CrLfToken token;
                if (lexer.TryRead(out token))
                {
                    Assert.Fail();
                }

                Assert.IsNull(token);
            }
        }
    }
}
