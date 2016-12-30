using Txt.ABNF;
using Txt.Core;

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
