﻿namespace SLANG.Core
{
    using Xunit;

    public class CharacterLexerTests
    {
        [Theory]
        [InlineData("\x01")]
        [InlineData("\x10")]
        [InlineData("\x20")]
        [InlineData("\x30")]
        [InlineData("\x40")]
        [InlineData("\x50")]
        [InlineData("\x60")]
        [InlineData("\x70")]
        [InlineData("\x7F")]
        public void ReadSuccess(string c)
        {
            var lexer = new CharacterLexer(new CharacterValueRangeLexer());
            using (var scanner = new TextScanner(new PushbackInputStream(c.ToMemoryStream())))
            {
                scanner.Read();
                var character = lexer.Read(scanner);
                Assert.NotNull(character);
                Assert.Equal(c, character.Data);
            }
        }
    }
}