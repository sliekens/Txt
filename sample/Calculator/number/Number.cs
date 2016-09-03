using Txt.ABNF;

namespace Calculator.number
{
    public class Number : Alternation
    {
        public Number(Alternation number)
            : base(number)
        {
        }
    }
}
