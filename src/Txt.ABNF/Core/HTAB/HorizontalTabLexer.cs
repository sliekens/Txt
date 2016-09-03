using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF.Core.HTAB
{
    public class HorizontalTabLexer : CompositeLexer<Terminal, HorizontalTab>
    {
        public HorizontalTabLexer([NotNull] ILexer<Terminal> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
