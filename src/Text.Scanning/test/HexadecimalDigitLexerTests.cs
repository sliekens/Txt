using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Text.Scanning;
using Text.Scanning.Core;

namespace Text
{
    [TestClass]
    public class HexadecimalDigitLexerTests
    {
        [TestMethod]
        public void ReadHexDigs()
        {
            var text = "0123456789ABCDEF";
            using (var reader = new StringReader(text))
            using (ITextScanner scanner = new TextScanner(reader))
            {
                scanner.Read();
                var lexer = new HexadecimalDigitLexer();
                while (!scanner.EndOfInput)
                {
                    var element = lexer.Read(scanner);
                    Assert.IsNotNull(element);
                    Assert.IsTrue(Uri.IsHexDigit(element.Data[0]));
                }
            }
        }
    }
}
