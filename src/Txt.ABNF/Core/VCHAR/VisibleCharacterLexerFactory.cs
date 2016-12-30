using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Txt.ABNF.Core.VCHAR
{
    public sealed class VisibleCharacterLexerFactory : RuleLexerFactory<VisibleCharacter>
    {
        static VisibleCharacterLexerFactory()
        {
            Default = new VisibleCharacterLexerFactory();
        }

        [NotNull]
        public static VisibleCharacterLexerFactory Default { get; }

        public override ILexer<VisibleCharacter> Create()
        {
            var innerLexer = ValueRange.Create('\x21', '\x7E');
            return new VisibleCharacterLexer(innerLexer);
        }
    }
}
