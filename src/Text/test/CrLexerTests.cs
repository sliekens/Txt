using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Text.Core;

namespace Text
{
    [TestClass]
    public class CrLexerTests
    {
        [TestMethod]
        public void ReadCr()
        {
            var text = "\r";
            using (var reader = new StringReader(text))
            using (var scanner = new TextScanner(reader))
            {
                scanner.Read();
                var lexer = new CrLexer(scanner);
                var token = lexer.Read();
                Assert.AreEqual(text, token.Data);
            }
        }

        [TestMethod]
        public void FailLf()
        {
            var text = "\n";
            using (var reader = new StringReader(text))
            using (var scanner = new TextScanner(reader))
            {
                scanner.Read();
                var lexer = new CrLexer(scanner);
                CrToken token;
                if (lexer.TryRead(out token))
                {
                    Assert.Fail();
                }

                Assert.IsNull(token);
            }
        }
    }
}
