﻿namespace SLANG
{
    using System.IO;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using SLANG.Core;

    [TestClass]
    public class ControlCharacterLexerTests
    {
        [TestMethod]
        public void ReadCtl()
        {
            var text = "\x01";
            var lexer = new ControlCharacterLexer();
            using (var reader = new StringReader(text))
            using (ITextScanner scanner = new TextScanner(reader))
            {
                scanner.Read();
                var element = lexer.Read(scanner);
                Assert.AreEqual(text, element.Data);
            }
        }

        [TestMethod]
        public void FailAlpha()
        {
            var text = "A";
            var lexer = new ControlCharacterLexer();
            using (var reader = new StringReader(text))
            using (ITextScanner scanner = new TextScanner(reader))
            {
                scanner.Read();
                ControlCharacter element;
                if (lexer.TryRead(scanner, out element))
                {
                    Assert.Fail();
                }

                Assert.IsNull(element);
            }
        }
    }
}
