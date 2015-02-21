using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Text.Scanning;
using Text.Scanning.Core;

namespace Text
{
    [TestClass]
    public class DigitLexerTests
    {
        [TestMethod]
        public void ReadDigits()
        {
            var text = "0123456789";
            var lexer = new DigitLexer();
            using (var reader = new StringReader(text))
            using (ITextScanner scanner = new TextScanner(reader))
            {
                scanner.Read();
                while (!scanner.EndOfInput)
                {
                    var element = lexer.Read(scanner);
                    Assert.IsNotNull(element);
                    Assert.IsTrue(char.IsDigit(element.Data, 0));
                }
            }
        }
    }
}
