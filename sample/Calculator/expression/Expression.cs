using Txt.ABNF;

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
