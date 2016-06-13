using Txt.ABNF;
using Txt.Core;

namespace Sample1.number
{
    public sealed class NumberLexer : CompositeLexer<Repetition, Number>
    {
        public NumberLexer(ILexer<Repetition> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
