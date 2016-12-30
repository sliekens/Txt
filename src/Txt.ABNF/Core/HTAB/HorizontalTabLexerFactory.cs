using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Txt.ABNF.Core.HTAB
{
    public sealed class HorizontalTabLexerFactory : RuleLexerFactory<HorizontalTab>
    {
        static HorizontalTabLexerFactory()
        {
            Default = new HorizontalTabLexerFactory();
        }

        [NotNull]
        public static HorizontalTabLexerFactory Default { get; }

        public override ILexer<HorizontalTab> Create()
        {
            var innerLexer = Terminal.Create("\x09", StringComparer.Ordinal);
            return new HorizontalTabLexer(innerLexer);
        }
    }
}
