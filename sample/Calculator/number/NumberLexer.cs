using Txt.ABNF;
using Txt.Core;

namespace Calculator.number
{
    public sealed class NumberLexer : CompositeLexer<Repetition, Number>
    {
        public NumberLexer(ILexer<Repetition> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
