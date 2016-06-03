using Sample1.term;
using Txt.ABNF;
using Txt.Core;

namespace Sample1.expression
{
    public class ExpressionParser : Parser<Expression, double>
    {
        private readonly IParser<Term, double> termParser;

        public ExpressionParser(IParser<Term, double> termParser)
        {
            this.termParser = termParser;
        }

        protected override double ParseImpl(Expression expression)
        {
            var left = termParser.Parse((Term)expression[0]);
            var repetition = (Repetition)expression[1];
            foreach (Concatenation concatenation in repetition)
            {
                var sign = concatenation[0];
                var term = (Term)concatenation[1];
                var right = termParser.Parse(term);
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
    }
}
