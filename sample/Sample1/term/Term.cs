using Sample1.factor;
using Txt.ABNF;

namespace Sample1.term
{
    public class Term : Concatenation
    {
        public Term(Concatenation concatenation)
            : base(concatenation)
        {
        }

        public decimal GetValue()
        {
            var value = ((Factor)Elements[0]).GetValue();
            var repetition = (Repetition)Elements[1];
            foreach (Concatenation concatenation in repetition)
            {
                var sign = concatenation[0];
                var factor = (Factor)concatenation[1];
                if (sign.Text == "*")
                {
                    value *= factor.GetValue();
                }
                else if (sign.Text == "/")
                {
                    value /= factor.GetValue();
                }
            }
            return value;
        }
    }
}
