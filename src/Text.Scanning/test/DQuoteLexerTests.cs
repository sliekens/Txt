using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Text.Scanning;
using Text.Scanning.Core;

namespace Text
{
    [TestClass]
    public class DQuoteLexerTests
    {
        [TestMethod]
        public void ReadDQuote()
        {
            var text = "\"";
            using (var reader = new StringReader(text))
            using (ITextScanner scanner = new TextScanner(reader))
            {
                scanner.Read();
                var lexer = new DQuoteLexer(scanner);
                var token = lexer.Read();
                Assert.IsNotNull(token);
                Assert.AreEqual(text, token.Data);
            }
        }
    }
}
