using Sample1.expression;
using Sample1.number;
using Txt.ABNF;

namespace Sample1.factor
{
    public class Factor : Concatenation
    {
        public Factor(Concatenation concatenation)
            : base(concatenation)
        {
        }

        public decimal GetValue()
        {
            decimal value = 0;
            var alt = Elements[1] as Alternation;
            if (alt.Ordinal == 1)
            {
                var number = (Number)alt.Element;
                value = number.GetValue();
            }

            if (alt.Ordinal == 2)
            {
                var concatenation = (Concatenation)alt.Element;
                var expression = (Expression)concatenation[1];
                value = expression.GetValue();
            }

            if (Elements[0].Text == "-")
            {
                value *= -1;
            }

            return value;
        }
    }
}
