using Txt.ABNF;
using Txt.Core;

namespace Calculator.number
{
    public sealed class NumberLexer : CompositeLexer<Alternation, Number>
    {
        public NumberLexer(ILexer<Alternation> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
