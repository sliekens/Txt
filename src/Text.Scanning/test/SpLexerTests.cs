using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Text.Scanning;
using Text.Scanning.Core;

namespace Text
{
    [TestClass]
    public class SpLexerTests
    {
        [TestMethod]
        public void ReadSp()
        {
            var text = " ";
            var lexer = new SpLexer();
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
