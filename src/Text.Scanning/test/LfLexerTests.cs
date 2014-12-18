﻿using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Text.Scanning;
using Text.Scanning.Core;

namespace Text
{
    [TestClass]
    public class LfLexerTests
    {
        [TestMethod]
        public void ReadLf()
        {
            var text = "\n";
            using (var reader = new StringReader(text))
            using (ITextScanner scanner = new TextScanner(reader))
            {
                scanner.Read();
                var lexer = new LfLexer(scanner);
                var token = lexer.Read();
                Assert.IsNotNull(token);
                Assert.AreEqual("\n", token.Data);
            }
        }
    }
}