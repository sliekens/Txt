using Txt.ABNF;

namespace Sample1.expression
{
    public class Expression : Concatenation
    {
        public Expression(Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}
