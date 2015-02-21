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
            using (ITextScanner scanner = new TextScanner(reader))
            {
                scanner.Read();
                var lexer = new WSpLexer();
                var element = lexer.Read(scanner);
                Assert.IsNotNull(element);
                Assert.IsNotNull(element.Sp);
                Assert.IsNull(element.HTab);
                Assert.AreEqual(text, element.Data);
            }
        }

        [TestMethod]
        public void ReadHTab()
        {
            var text = "\t";
            using (var reader = new StringReader(text))
            using (ITextScanner scanner = new TextScanner(reader))
            {
                scanner.Read();
                var lexer = new WSpLexer();
                var element = lexer.Read(scanner);
                Assert.IsNotNull(element);
                Assert.IsNotNull(element.HTab);
                Assert.IsNull(element.Sp);
                Assert.AreEqual(text, element.Data);
            }
        }
    }
}
