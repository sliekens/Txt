using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Text.Scanning;
using Text.Scanning.Core;

namespace Text
{
    [TestClass]
    public class WSpLexerTests
    {
        [TestMethod]
        public void ReadSp()
        {
            var text = " ";
            using (var reader = new StringReader(text))
            using (var scanner = new TextScanner(reader))
            {
                scanner.Read();
                var lexer = new WSpLexer(scanner);
                var token = lexer.Read();
                Assert.IsNotNull(token);
                Assert.IsNotNull(token.Sp);
                Assert.IsNull(token.HTab);
                Assert.AreEqual(text, token.Data);
            }
        }

        [TestMethod]
        public void ReadHTab()
        {
            var text = "\t";
            using (var reader = new StringReader(text))
            using (var scanner = new TextScanner(reader))
            {
                scanner.Read();
                var lexer = new WSpLexer(scanner);
                var token = lexer.Read();
                Assert.IsNotNull(token);
                Assert.IsNotNull(token.HTab);
                Assert.IsNull(token.Sp);
                Assert.AreEqual(text, token.Data);
            }
        }
    }
}
