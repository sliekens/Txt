using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Text.Core;

namespace Text
{
    [TestClass]
    public class DigitLexerTests
    {
        [TestMethod]
        public void ReadDigits()
        {
            var text = "0123456789";
            using (var reader = new StringReader(text))
            using (var scanner = new TextScanner(reader))
            {
                scanner.Read();
                var lexer = new DigitLexer(scanner);
                while (!scanner.EndOfInput)
                {
                    var token = lexer.Read();
                    Assert.IsNotNull(token);
                    Assert.IsTrue(char.IsDigit(token.Data, 0));
                }
            }
        }
    }
}
