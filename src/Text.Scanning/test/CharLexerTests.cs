using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Text.Scanning;
using Text.Scanning.Core;

namespace Text
{
    [TestClass]
    public class CharLexerTests
    {
        [TestMethod]
        public void ReadFirstAsciiChar()
        {
            var text = char.ToString(Convert.ToChar(0x01));
            using (var reader = new StringReader(text))
            using (ITextScanner scanner = new TextScanner(reader))
            {
                scanner.Read();
                var lexer = new CharLexer(scanner);
                var token = lexer.Read();
                Assert.AreEqual(text, token.Data);
            }
        }

        [TestMethod]
        public void ReadLastAsciiChar()
        {
            var text = char.ToString(Convert.ToChar(0x7F));
            using (var reader = new StringReader(text))
            using (ITextScanner scanner = new TextScanner(reader))
            {
                scanner.Read();
                var lexer = new CharLexer(scanner);
                var token = lexer.Read();
                Assert.AreEqual(text, token.Data);
            }
        }

        [TestMethod]
        public void FailNulChar()
        {
            var text = char.ToString('\0');
            using (var reader = new StringReader(text))
            using (ITextScanner scanner = new TextScanner(reader))
            {
                scanner.Read();
                var lexer = new CharLexer(scanner);
                CharToken token;
                if (lexer.TryRead(out token))
                {
                    Assert.Fail();
                }

                Assert.IsNull(token);
            }
        }
    }
}
