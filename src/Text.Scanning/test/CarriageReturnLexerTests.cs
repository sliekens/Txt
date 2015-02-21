using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Text.Scanning;
using Text.Scanning.Core;

namespace Text
{
    [TestClass]
    public class CarriageReturnLexerTests
    {
        [TestMethod]
        public void ReadCr()
        {
            var text = "\r";
            var lexer = new CarriageReturnLexer();
            using (var reader = new StringReader(text))
            using (ITextScanner scanner = new TextScanner(reader))
            {
                scanner.Read();
                var element = lexer.Read(scanner);
                Assert.AreEqual(text, element.Data);
            }
        }

        [TestMethod]
        public void FailLf()
        {
            var text = "\n";
            var lexer = new CarriageReturnLexer();
            using (var reader = new StringReader(text))
            using (ITextScanner scanner = new TextScanner(reader))
            {
                scanner.Read();
                CarriageReturn element;
                if (lexer.TryRead(scanner, out element))
                {
                    Assert.Fail();
                }

                Assert.IsNull(element);
            }
        }
    }
}
