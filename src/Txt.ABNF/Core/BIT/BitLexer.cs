using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF.Core.BIT
{
    public class BitLexer : CompositeLexer<Alternation, Bit>
    {
        public BitLexer([NotNull] ILexer<Alternation> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
