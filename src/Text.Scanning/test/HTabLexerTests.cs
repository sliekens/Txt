using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Text.Scanning;
using Text.Scanning.Core;

namespace Text
{
    [TestClass]
    public class HTabLexerTests
    {
        [TestMethod]
        public void ReadHTab()
        {
            var text = "\t";
            var lexer = new HTabLexer();
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
