using Sample1.term;
using Txt.ABNF;
using Txt.Core;

namespace Sample1.expression
{
    public class ExpressionParser : Parser<Expression, decimal>
    {
        private readonly IParser<Term, decimal> termParser;

        public ExpressionParser(IParser<Term, decimal> termParser)
        {
            this.termParser = termParser;
        }

        protected override decimal ParseImpl(Expression expression)
        {
            var value = termParser.Parse((Term)expression[0]);
            var repetition = (Repetition)expression[1];
            foreach (Concatenation concatenation in repetition)
            {
                var sign = concatenation[0];
                var term = (Term)concatenation[1];
                if (sign.Text == "+")
                {
                    value += termParser.Parse(term);
                }
                else if (sign.Text == "-")
                {
                    value -= termParser.Parse(term);
                }
            }
            return value;
        }
    }
}
