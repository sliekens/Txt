using Txt.ABNF;
using Txt.Core;

namespace Calculator.number
{
    public class Number : Alternation
    {
        public Number(Alternation alternation)
            : base(alternation)
        {
        }
    }
}
