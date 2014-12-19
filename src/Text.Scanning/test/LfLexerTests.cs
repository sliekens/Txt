using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Text.Scanning;
using Text.Scanning.Core;

namespace Text
{
    [TestClass]
    public class LfLexerTests
    {
        [TestMethod]
        public void ReadLf()
        {
            var text = "\n";
            var lexer = new LfLexer();
            using (var reader = new StringReader(text))
            using (ITextScanner scanner = new TextScanner(reader))
            {
                scanner.Read();
                var token = lexer.Read(scanner);
                Assert.IsNotNull(token);
                Assert.AreEqual("\n", token.Data);
            }
        }
    }
}
