namespace SLANG
{
    using System.IO;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using SLANG.Core;

    [TestClass]
    public class HorizontalTabLexerTests
    {
        [TestMethod]
        public void ReadHTab()
        {
            var text = "\t";
            var lexer = new HorizontalTabLexer();
            using (var reader = new StringReader(text))
            using (ITextScanner scanner = new TextScanner(reader))
            {
                scanner.Read();
                var element = lexer.Read(scanner);
                Assert.IsNotNull(element);
                Assert.AreEqual(text, element.Data);
            }
        }
    }
}
