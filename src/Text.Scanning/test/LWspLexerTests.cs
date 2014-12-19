using System;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Text.Scanning;
using Text.Scanning.Core;

namespace Text
{
    [TestClass]
    public class LWspLexerTests
    {
        [TestMethod]
        public void ReadSp()
        {
            var text = " ";
            using (var reader = new StringReader(text))
            using (ITextScanner scanner = new TextScanner(reader))
            {
                scanner.Read();
                var lexer = new LWspLexer();
                var token = lexer.Read(scanner);
                Assert.IsNotNull(token);
                Assert.IsNotNull(token.LWsp);
                Assert.AreEqual(text, token.Data);
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
                var lexer = new LWspLexer();
                var token = lexer.Read(scanner);
                Assert.IsNotNull(token);
                Assert.IsNotNull(token.LWsp);
                Assert.AreEqual(text, token.Data);
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
                var lexer = new LWspLexer();
                var token = lexer.Read(scanner);
                Assert.IsNotNull(token);
                Assert.IsNotNull(token.LWsp);
                Assert.AreEqual(text, token.Data);
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
                var lexer = new LWspLexer();
                var token = lexer.Read(scanner);
                Assert.IsNotNull(token);
                Assert.IsNotNull(token.LWsp);
                Assert.IsFalse(token.LWsp.Any());
                Assert.AreEqual(text, token.Data);
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
                var lexer = new LWspLexer();
                var token = lexer.Read(scanner);
                Assert.IsFalse(token.LWsp.Any());
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
                var lexer = new LWspLexer();
                LWspToken token;
                if (!lexer.TryRead(scanner, out token))
                {
                    Assert.Fail();
                }

                Assert.IsNotNull(token);
                Assert.IsNotNull(token.LWsp);
                Assert.IsFalse(token.LWsp.Any());
            }
        }
    }
}
