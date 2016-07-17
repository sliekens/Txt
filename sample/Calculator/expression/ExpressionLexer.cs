using Txt.ABNF;
using Txt.Core;

namespace Calculator.expression
{
    public sealed class ExpressionLexer : CompositeLexer<Concatenation, Expression>
    {
        public ExpressionLexer(ILexer<Concatenation> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
