using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Text.Core;

namespace Text
{
    [TestClass]
    public class AlphaLexerTests
    {
        [TestMethod]
        public void ReadLowerCaseAlphabet()
        {
            var text = "abcdefghijklmnopqrstuvwxyz";
            using (var reader = new StringReader(text))
            using (var scanner = new TextScanner(reader))
            {
                scanner.Read();
                var lexer = new AlphaLexer(scanner);
                for (int i = 0; i < text.Length; i++)
                {
                    var token = lexer.Read();
                    Assert.AreEqual(text.Substring(i, 1), token.Data);
                }
            }
        }

        [TestMethod]
        public void ReadUpperCaseAlphabet()
        {
            var text = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            using (var reader = new StringReader(text))
            using (var scanner = new TextScanner(reader))
            {
                scanner.Read();
                var lexer = new AlphaLexer(scanner);
                for (int i = 0; i < text.Length; i++)
                {
                    var token = lexer.Read();
                    Assert.AreEqual(text.Substring(i, 1), token.Data);
                }
            }
        }

        [TestMethod]
        public void FailSymbol()
        {
            var text = "%";
            using (var reader = new StringReader(text))
            using (var scanner = new TextScanner(reader))
            {
                scanner.Read();
                var lexer = new AlphaLexer(scanner);
                AlphaToken token;
                var result = lexer.TryRead(out token);
                Assert.IsFalse(result);
                Assert.IsNull(token);
            }
        }
    }
}
