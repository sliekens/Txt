using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Text
{
    [TestClass]
    public class WithInternetLineEndings
    {
        [TestMethod]
        public void ReadEmptyString()
        {
            using (StringReader reader = new StringReader(string.Empty))
            using (ITextScanner scanner = new TextScanner(reader, EndOfLine.CrLf))
            {
                Assert.IsFalse(scanner.Read());
                Assert.IsTrue(scanner.EndOfInput);
            }
        }

        [TestMethod]
        public void ReadHelloWorld()
        {
            var text = "Hello World!";
            var characters = text.ToCharArray();
            using (StringReader reader = new StringReader(text))
            using (ITextScanner scanner = new TextScanner(reader, EndOfLine.CrLf))
            {
                foreach (var character in characters)
                {
                    if (!scanner.Read())
                    {
                        Assert.Fail("Unexpected end of input");
                    }

                    Assert.AreEqual(character, scanner.NextCharacter);
                }
            }
        }

        [TestMethod]
        public void ReadEndOfLine()
        {
            var text = "\r\n";
            using (StringReader reader = new StringReader(text))
            using (ITextScanner scanner = new TextScanner(reader, EndOfLine.CrLf))
            {
                while (!scanner.EndOfInput)
                {
                    scanner.Read();
                }

                Assert.AreEqual(1, scanner.Line);
                Assert.AreEqual(0, scanner.Column);
            }
        }
    }
}
