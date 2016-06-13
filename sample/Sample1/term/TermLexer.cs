using Txt.ABNF;
using Txt.Core;

namespace Sample1.term
{
    public sealed class TermLexer : CompositeLexer<Concatenation, Term>
    {
        public TermLexer(ILexer<Concatenation> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
