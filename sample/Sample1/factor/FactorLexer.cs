using Txt.ABNF;
using Txt.Core;

namespace Sample1.factor
{
    public sealed class FactorLexer : CompositeLexer<Concatenation, Factor>
    {
        public FactorLexer(ILexer<Concatenation> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
