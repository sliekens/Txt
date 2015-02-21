using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Text.Scanning;
using Text.Scanning.Core;

namespace Text
{
    [TestClass]
    public class WhiteSpaceLexerTests
    {
        [TestMethod]
        public void ReadSp()
        {
            var text = " ";
            using (var reader = new StringReader(text))
            using (ITextScanner scanner = new TextScanner(reader))
            {
                scanner.Read();
                var lexer = new WhiteSpaceLexer();
                var element = lexer.Read(scanner);
                Assert.IsNotNull(element);
                Assert.IsNotNull(element.Sp);
                Assert.IsNull(element.HorizontalTab);
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
                var lexer = new WhiteSpaceLexer();
                var element = lexer.Read(scanner);
                Assert.IsNotNull(element);
                Assert.IsNotNull(element.HorizontalTab);
                Assert.IsNull(element.Sp);
                Assert.AreEqual(text, element.Data);
            }
        }
    }
}
