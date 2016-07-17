using Txt.ABNF;

namespace Calculator.factor
{
    public class Factor : Concatenation
    {
        public Factor(Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}
