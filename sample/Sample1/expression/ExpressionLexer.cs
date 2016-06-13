using Txt.ABNF;
using Txt.Core;

namespace Sample1.expression
{
    public sealed class ExpressionLexer : CompositeLexer<Concatenation, Expression>
    {
        public ExpressionLexer(ILexer<Concatenation> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
