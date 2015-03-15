﻿namespace SLANG
{
    using System.IO;
    using Core;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class WhiteSpaceLexerTests
    {
        [TestMethod]
        public void ReadSpace()
        {
            var text = " ";
            using (var reader = new StringReader(text))
            using (ITextScanner scanner = new TextScanner(reader))
            {
                scanner.Read();
                var lexer = new WhiteSpaceLexer();
                var element = lexer.Read(scanner);
                Assert.IsNotNull(element);
                Assert.IsNotNull(element.Element);
                Assert.IsInstanceOfType(element.Element, typeof(Space));
                Assert.AreEqual(text, element.Data);
            }
        }

        [TestMethod]
        public void ReadHorizontalTab()
        {
            var text = "\t";
            using (var reader = new StringReader(text))
            using (ITextScanner scanner = new TextScanner(reader))
            {
                scanner.Read();
                var lexer = new WhiteSpaceLexer();
                var element = lexer.Read(scanner);
                Assert.IsNotNull(element);
                Assert.IsNotNull(element.Element);
                Assert.IsInstanceOfType(element.Element, typeof(HorizontalTab));
                Assert.AreEqual(text, element.Data);
            }
        }
    }
}