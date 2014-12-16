using System;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Text.Core;

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
            using (var scanner = new TextScanner(reader))
            {
                scanner.Read();
                var lexer = new LWspLexer(scanner);
                var token = lexer.Read();
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
            using (var scanner = new TextScanner(reader))
            {
                scanner.Read();
                var lexer = new LWspLexer(scanner);
                var token = lexer.Read();
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
            using (var scanner = new TextScanner(reader))
            {
                scanner.Read();
                var lexer = new LWspLexer(scanner);
                var token = lexer.Read();
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
            using (var scanner = new TextScanner(reader))
            {
                scanner.Read();
                var lexer = new LWspLexer(scanner);
                var token = lexer.Read();
                Assert.IsNotNull(token);
                Assert.IsNotNull(token.LWsp);
                Assert.IsFalse(token.LWsp.Any());
                Assert.AreEqual(text, token.Data);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(SyntaxErrorException))]
        public void FailCrLf()
        {
            var text = "\r\n";
            using (var reader = new StringReader(text))
            using (var scanner = new TextScanner(reader))
            {
                scanner.Read();
                var lexer = new LWspLexer(scanner);
                lexer.Read();
            }
        }

        [TestMethod]
        public void FailTryCrLf()
        {
            var text = "\r\n";
            using (var reader = new StringReader(text))
            using (var scanner = new TextScanner(reader))
            {
                scanner.Read();
                var lexer = new LWspLexer(scanner);
                LWspToken token;
                if (lexer.TryRead(out token))
                {
                    Assert.Fail();
                }

                Assert.IsNotNull(token);
                Assert.IsNotNull(token.LWsp);
                Assert.IsInstanceOfType(token.LWsp.Last(), typeof(CrLfToken));
            }
        }
    }
}
