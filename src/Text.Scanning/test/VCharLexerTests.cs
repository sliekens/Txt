using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Text.Scanning;
using Text.Scanning.Core;

namespace Text
{
    [TestClass]
    public class VCharLexerTests
    {
        [TestMethod]
        public void ReadAlpha()
        {
            var text = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var lexer = new VCharLexer();
            using (var reader = new StringReader(text))
            using (ITextScanner scanner = new TextScanner(reader))
            {
                scanner.Read();
                while (!scanner.EndOfInput)
                {
                    var token = lexer.Read(scanner);
                    Assert.IsNotNull(token);
                    Assert.IsTrue(char.IsLetter(token.Data[0]));
                }
            }
        }

        [TestMethod]
        public void FailHTab()
        {
            var text = "\t";
            var lexer = new VCharLexer();
            using (var reader = new StringReader(text))
            using (ITextScanner scanner = new TextScanner(reader))
            {
                scanner.Read();
                VCharToken token;
                if (lexer.TryRead(scanner, out token))
                {
                    Assert.Fail();
                }

                Assert.IsNull(token);
            }
        }
    }
}
