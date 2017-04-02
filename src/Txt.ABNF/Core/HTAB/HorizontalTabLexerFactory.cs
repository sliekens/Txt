using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF.Core.HTAB
{
    public sealed class HorizontalTabLexerFactory : LexerFactory<HorizontalTab>
    {
        static HorizontalTabLexerFactory()
        {
            Default = new HorizontalTabLexerFactory();
        }

        [NotNull]
        public static HorizontalTabLexerFactory Default { get; }

        public override ILexer<HorizontalTab> Create()
        {
            return new HorizontalTabLexer();
        }
    }
}
