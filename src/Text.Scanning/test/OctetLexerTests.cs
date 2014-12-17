using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Text.Scanning;
using Text.Scanning.Core;

namespace Text
{
    [TestClass]
    public class OctetLexerTests
    {
        [TestMethod]
        public void ReadNul()
        {
            var text = "\0";
            using (var reader = new StringReader(text))
            using (ITextScanner scanner = new TextScanner(reader))
            {
                scanner.Read();
                var lexer = new OctetLexer(scanner);
                var token = lexer.Read();
                Assert.IsNotNull(token);
                Assert.AreEqual(text, token.Data);
            }
        }

        [TestMethod]
        public void Read255()
        {
            var text = char.ToString((char)0xFF);
            using (var reader = new StringReader(text))
            using (ITextScanner scanner = new TextScanner(reader))
            {
                scanner.Read();
                var lexer = new OctetLexer(scanner);
                var token = lexer.Read();
                Assert.IsNotNull(token);
                Assert.AreEqual(text, token.Data);
            }
        }
    }
}
