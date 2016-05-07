using System;
using Txt.ABNF;
using Txt.Core;
using Txt.UTF8.UTF8_char;

namespace Txt.UTF8.UTF8_octets
{
    public class Utf8OctetsLexerFactory : ILexerFactory<Utf8Octets>
    {
        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        private readonly ILexer<Utf8Character> utf8CharacterLexer;

        public Utf8OctetsLexerFactory(
            IRepetitionLexerFactory repetitionLexerFactory,
            ILexer<Utf8Character> utf8CharacterLexer)
        {
            if (repetitionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(repetitionLexerFactory));
            }
            if (utf8CharacterLexer == null)
            {
                throw new ArgumentNullException(nameof(utf8CharacterLexer));
            }
            this.repetitionLexerFactory = repetitionLexerFactory;
            this.utf8CharacterLexer = utf8CharacterLexer;
        }

        public ILexer<Utf8Octets> Create()
        {
            return new Utf8OctetsLexer(repetitionLexerFactory.Create(utf8CharacterLexer, 0, int.MaxValue));
        }
    }
}
