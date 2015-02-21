﻿using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Text.Scanning;
using Text.Scanning.Core;

namespace Text
{
    [TestClass]
    public class CrLfLexerTests
    {
        [TestMethod]
        public void ReadCrLf()
        {
            var text = "\r\n";
            using (var reader = new StringReader(text))
            using (ITextScanner scanner = new TextScanner(reader))
            {
                scanner.Read();
                var lexer = new CrLfLexer();
                var element = lexer.Read(scanner);
                Assert.IsNotNull(element);
                Assert.AreEqual("\r\n", element.Data);
            }
        }

        [TestMethod]
        public void FailCr()
        {
            var text = "\r";
            using (var reader = new StringReader(text))
            using (ITextScanner scanner = new TextScanner(reader))
            {
                scanner.Read();
                var lexer = new CrLfLexer();
                CrLfElement element;
                if (lexer.TryRead(scanner, out element))
                {
                    Assert.Fail();
                }

                Assert.IsNull(element);
            }
        }

        [TestMethod]
        public void FailLf()
        {
            var text = "\n";
            using (var reader = new StringReader(text))
            using (ITextScanner scanner = new TextScanner(reader))
            {
                scanner.Read();
                var lexer = new CrLfLexer();
                CrLfElement element;
                if (lexer.TryRead(scanner, out element))
                {
                    Assert.Fail();
                }

                Assert.IsNull(element);
            }
        }
    }
}
