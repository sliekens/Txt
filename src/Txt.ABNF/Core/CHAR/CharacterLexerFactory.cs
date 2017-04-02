using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF.Core.CHAR
{
    public sealed class CharacterLexerFactory : LexerFactory<Character>
    {
        static CharacterLexerFactory()
        {
            Default = new CharacterLexerFactory();
        }

        [NotNull]
        public static CharacterLexerFactory Default { get; }

        public override ILexer<Character> Create()
        {
            return new CharacterLexer();
        }
    }
}
