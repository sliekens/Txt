namespace SLANG
{
    using System;
    using System.IO;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using SLANG.Core;

    [TestClass]
    public class HexadecimalDigitLexerTests
    {
        [TestMethod]
        public void ReadHexDigs()
        {
            var text = "0123456789ABCDEF";
            using (var inputStream = text.AsStream())
            using (var pushbackInputStream = new PushbackInputStream(inputStream))
            using (ITextScanner scanner = new TextScanner(pushbackInputStream))
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
