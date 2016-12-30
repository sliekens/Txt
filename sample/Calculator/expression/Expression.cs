using Txt.ABNF;
using Txt.Core;

namespace Calculator.expression
{
    public class Expression : Concatenation
    {
        public Expression(Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}
