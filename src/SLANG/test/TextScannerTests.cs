namespace SLANG
{
    using System.IO;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void ReadEmptyString()
        {
            using (var inputStream = string.Empty.AsStream())
            using (var pushbackInputStream = new PushbackInputStream(inputStream))
            using (ITextScanner scanner = new TextScanner(pushbackInputStream))
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
            using (var inputStream = text.AsStream())
            using (var pushbackInputStream = new PushbackInputStream(inputStream))
            using (ITextScanner scanner = new TextScanner(pushbackInputStream))
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
