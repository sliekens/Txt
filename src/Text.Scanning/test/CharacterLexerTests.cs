using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Text.Scanning;
using Text.Scanning.Core;

namespace Text
{
    [TestClass]
    public class CharacterLexerTests
    {
        [TestMethod]
        public void ReadFirstAsciiChar()
        {
            var text = char.ToString(Convert.ToChar(0x01));
            var lexer = new CharacterLexer();
            using (var reader = new StringReader(text))
            using (ITextScanner scanner = new TextScanner(reader))
            {
                scanner.Read();
                var element = lexer.Read(scanner);
                Assert.AreEqual(text, element.Data);
            }
        }

        [TestMethod]
        public void ReadLastAsciiChar()
        {
            var text = char.ToString(Convert.ToChar(0x7F));
            var lexer = new CharacterLexer();
            using (var reader = new StringReader(text))
            using (ITextScanner scanner = new TextScanner(reader))
            {
                scanner.Read();
                var element = lexer.Read(scanner);
                Assert.AreEqual(text, element.Data);
            }
        }

        [TestMethod]
        public void FailNulChar()
        {
            var text = char.ToString('\0');
            var lexer = new CharacterLexer();
            using (var reader = new StringReader(text))
            using (ITextScanner scanner = new TextScanner(reader))
            {
                scanner.Read();
                Character element;
                if (lexer.TryRead(scanner, out element))
                {
                    Assert.Fail();
                }

                Assert.IsNull(element);
            }
        }
    }
}
