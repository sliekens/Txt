using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Text.Core;

namespace Text
{
    [TestClass]
    public class VCharLexerTests
    {
        [TestMethod]
        public void ReadAlpha()
        {
            var text = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            using (var reader = new StringReader(text))
            using (var scanner = new TextScanner(reader))
            {
                scanner.Read();
                var lexer = new VCharLexer(scanner);
                while (!scanner.EndOfInput)
                {
                    var token = lexer.Read();
                    Assert.IsNotNull(token);
                    Assert.IsTrue(char.IsLetter(token.Data[0]));
                }
            }
        }

        [TestMethod]
        public void FailHTab()
        {
            var text = "\t";
            using (var reader = new StringReader(text))
            using (var scanner = new TextScanner(reader))
            {
                scanner.Read();
                var lexer = new VCharLexer(scanner);
                VCharToken token;
                if (lexer.TryRead(out token))
                {
                    Assert.Fail();
                }

                Assert.IsNull(token);
            }
        }
    }
}
