using Txt.ABNF;

namespace Calculator.number
{
    public class Number : Repetition
    {
        public Number(Repetition repetition)
            : base(repetition)
        {
        }
    }
}
