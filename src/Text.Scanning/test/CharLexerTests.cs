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
            var lexer = new CharLexer();
            using (var reader = new StringReader(text))
            using (ITextScanner scanner = new TextScanner(reader))
            {
                scanner.Read();
                var token = lexer.Read(scanner);
                Assert.AreEqual(text, token.Data);
            }
        }

        [TestMethod]
        public void ReadLastAsciiChar()
        {
            var text = char.ToString(Convert.ToChar(0x7F));
            var lexer = new CharLexer();
            using (var reader = new StringReader(text))
            using (ITextScanner scanner = new TextScanner(reader))
            {
                scanner.Read();
                var token = lexer.Read(scanner);
                Assert.AreEqual(text, token.Data);
            }
        }

        [TestMethod]
        public void FailNulChar()
        {
            var text = char.ToString('\0');
            var lexer = new CharLexer();
            using (var reader = new StringReader(text))
            using (ITextScanner scanner = new TextScanner(reader))
            {
                scanner.Read();
                CharToken token;
                if (lexer.TryRead(scanner, out token))
                {
                    Assert.Fail();
                }

                Assert.IsNull(token);
            }
        }
    }
}
