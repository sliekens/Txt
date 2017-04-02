using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF.Core.VCHAR
{
    public sealed class VisibleCharacterLexerFactory : LexerFactory<VisibleCharacter>
    {
        static VisibleCharacterLexerFactory()
        {
            Default = new VisibleCharacterLexerFactory();
        }

        [NotNull]
        public static VisibleCharacterLexerFactory Default { get; }

        public override ILexer<VisibleCharacter> Create()
        {
            return new VisibleCharacterLexer();
        }
    }
}
