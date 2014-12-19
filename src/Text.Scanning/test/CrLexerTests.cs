using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Text.Scanning;
using Text.Scanning.Core;

namespace Text
{
    [TestClass]
    public class CrLexerTests
    {
        [TestMethod]
        public void ReadCr()
        {
            var text = "\r";
            var lexer = new CrLexer();
            using (var reader = new StringReader(text))
            using (ITextScanner scanner = new TextScanner(reader))
            {
                scanner.Read();
                var token = lexer.Read(scanner);
                Assert.AreEqual(text, token.Data);
            }
        }

        [TestMethod]
        public void FailLf()
        {
            var text = "\n";
            var lexer = new CrLexer();
            using (var reader = new StringReader(text))
            using (ITextScanner scanner = new TextScanner(reader))
            {
                scanner.Read();
                CrToken token;
                if (lexer.TryRead(scanner, out token))
                {
                    Assert.Fail();
                }

                Assert.IsNull(token);
            }
        }
    }
}
