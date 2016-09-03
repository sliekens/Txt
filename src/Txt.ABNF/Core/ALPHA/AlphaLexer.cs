using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF.Core.ALPHA
{
    public class AlphaLexer : CompositeLexer<Alternation, Alpha>
    {
        public AlphaLexer([NotNull] ILexer<Alternation> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
