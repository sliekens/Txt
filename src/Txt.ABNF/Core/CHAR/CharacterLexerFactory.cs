using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Txt.ABNF.Core.CHAR
{
    public sealed class CharacterLexerFactory : RuleLexerFactory<Character>
    {
        static CharacterLexerFactory()
        {
            Default = new CharacterLexerFactory();
        }

        [NotNull]
        public static CharacterLexerFactory Default { get; }

        public override ILexer<Character> Create()
        {
            var innerLexer = ValueRange.Create('\x01', '\x7F');
            return new CharacterLexer(innerLexer);
        }
    }
}
