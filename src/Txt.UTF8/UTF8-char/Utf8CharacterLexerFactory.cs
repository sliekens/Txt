using System;
using Txt.ABNF;
using Txt.Core;
using Txt.UTF8.UTF8_1;
using Txt.UTF8.UTF8_2;
using Txt.UTF8.UTF8_3;
using Txt.UTF8.UTF8_4;

namespace Txt.UTF8.UTF8_char
{
    public class Utf8CharacterLexerFactory : ILexerFactory<Utf8Character>
    {
        private readonly IAlternationLexerFactory alternationLexerFactory;

        private readonly ILexer<Utf8Octet1> utf8Octet1Lexer;

        private readonly ILexer<Utf8Octet2> utf8Octet2Lexer;

        private readonly ILexer<Utf8Octet3> utf8Octet3Lexer;

        private readonly ILexer<Utf8Octet4> utf8Octet4Lexer;

        public Utf8CharacterLexerFactory(
            IAlternationLexerFactory alternationLexerFactory,
            ILexer<Utf8Octet1> utf8Octet1Lexer,
            ILexer<Utf8Octet2> utf8Octet2Lexer,
            ILexer<Utf8Octet3> utf8Octet3Lexer,
            ILexer<Utf8Octet4> utf8Octet4Lexer)
        {
            if (alternationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alternationLexerFactory));
            }
            if (utf8Octet1Lexer == null)
            {
                throw new ArgumentNullException(nameof(utf8Octet1Lexer));
            }
            if (utf8Octet2Lexer == null)
            {
                throw new ArgumentNullException(nameof(utf8Octet2Lexer));
            }
            if (utf8Octet3Lexer == null)
            {
                throw new ArgumentNullException(nameof(utf8Octet3Lexer));
            }
            if (utf8Octet4Lexer == null)
            {
                throw new ArgumentNullException(nameof(utf8Octet4Lexer));
            }
            this.alternationLexerFactory = alternationLexerFactory;
            this.utf8Octet1Lexer = utf8Octet1Lexer;
            this.utf8Octet2Lexer = utf8Octet2Lexer;
            this.utf8Octet3Lexer = utf8Octet3Lexer;
            this.utf8Octet4Lexer = utf8Octet4Lexer;
        }

        public ILexer<Utf8Character> Create()
        {
            return
                new Utf8CharacterLexer(
                    alternationLexerFactory.Create(utf8Octet1Lexer, utf8Octet2Lexer, utf8Octet3Lexer, utf8Octet4Lexer));
        }
    }
}
