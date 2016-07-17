using Txt.ABNF;
using Txt.Core;

namespace Calculator.factor
{
    public sealed class FactorLexer : CompositeLexer<Concatenation, Factor>
    {
        public FactorLexer(ILexer<Concatenation> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
