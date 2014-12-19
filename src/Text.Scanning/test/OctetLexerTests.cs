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
            var lexer = new OctetLexer();
            using (var reader = new StringReader(text))
            using (ITextScanner scanner = new TextScanner(reader))
            {
                scanner.Read();
                var token = lexer.Read(scanner);
                Assert.IsNotNull(token);
                Assert.AreEqual(text, token.Data);
            }
        }

        [TestMethod]
        public void Read255()
        {
            var text = "\u00FF";
            var lexer = new OctetLexer();
            using (var reader = new StringReader(text))
            using (ITextScanner scanner = new TextScanner(reader))
            {
                scanner.Read();
                var token = lexer.Read(scanner);
                Assert.IsNotNull(token);
                Assert.AreEqual(text, token.Data);
            }
        }
    }
}
