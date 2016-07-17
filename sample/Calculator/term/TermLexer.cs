using Txt.ABNF;
using Txt.Core;

namespace Calculator.term
{
    public sealed class TermLexer : CompositeLexer<Concatenation, Term>
    {
        public TermLexer(ILexer<Concatenation> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
