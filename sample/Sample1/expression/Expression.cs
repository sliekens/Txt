using Sample1.term;
using Txt.ABNF;

namespace Sample1.expression
{
    public class Expression : Concatenation
    {
        public Expression(Concatenation concatenation)
            : base(concatenation)
        {
        }

        public decimal GetValue()
        {
            var value = ((Term)Elements[0]).GetValue();
            var repetition = (Repetition)Elements[1];
            foreach (Concatenation concatenation in repetition)
            {
                var sign = concatenation[0];
                var term = (Term)concatenation[1];
                if (sign.Text == "+")
                {
                    value += term.GetValue();
                }
                else if (sign.Text == "-")
                {
                    value -= term.GetValue();
                }
            }
            return value;
        }
    }
}
