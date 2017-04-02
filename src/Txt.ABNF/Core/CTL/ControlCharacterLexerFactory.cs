using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF.Core.CTL
{
    public sealed class ControlCharacterLexerFactory : LexerFactory<ControlCharacter>
    {
        static ControlCharacterLexerFactory()
        {
            Default = new ControlCharacterLexerFactory();
        }

        [NotNull]
        public static ControlCharacterLexerFactory Default { get; }

        public override ILexer<ControlCharacter> Create()
        {
            return new ControlCharacterLexer();
        }
    }
}
