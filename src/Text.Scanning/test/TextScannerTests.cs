using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Text.Scanning;

namespace Text
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void ReadEmptyString()
        {
            using (StringReader reader = new StringReader(string.Empty))
            using (ITextScanner scanner = new TextScanner(reader))
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
            using (ITextScanner scanner = new TextScanner(reader))
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
    }
}
