using Calculator.expression;
using Calculator.factor;
using Calculator.number;
using Calculator.term;
using Txt.ABNF;

namespace Calculator
{
    public class CalculatorParser
    {
        public double ParseExpression(Expression expression)
        {
            var left = ParseTerm((Term)expression[0]);
            var repetition = (Repetition)expression[1];
            foreach (Concatenation concatenation in repetition)
            {
                var sign = concatenation[0];
                var term = (Term)concatenation[1];
                var right = ParseTerm(term);
                if (sign.Text == "+")
                {
                    left += right;
                }
                else if (sign.Text == "-")
                {
                    left -= right;
                }
            }
            return left;
        }

        public double ParseFactor(Factor factor)
        {
            double value = 0;
            var alt = factor[1] as Alternation;
            if (alt.Ordinal == 1)
            {
                var number = (Number)alt.Element;
                value = ParseNumber(number);
            }
            if (alt.Ordinal == 2)
            {
                var concatenation = (Concatenation)alt.Element;
                var expression = (Expression)concatenation[1];
                value = ParseExpression(expression);
            }
            if (factor[0].Text == "-")
            {
                value *= -1;
            }
            return value;
        }

        public double ParseNumber(Number number)
        {
            return double.Parse(number.Text);
        }

        public double ParseTerm(Term term)
        {
            var left = ParseFactor((Factor)term[0]);
            var repetition = (Repetition)term[1];
            foreach (Concatenation concatenation in repetition)
            {
                var sign = concatenation[0];
                var factor = (Factor)concatenation[1];
                var right = ParseFactor(factor);
                if (sign.Text == "*")
                {
                    left *= right;
                }
                else if (sign.Text == "/")
                {
                    left /= right;
                }
            }
            return left;
        }
    }
}
