using Sample1.factor;
using Txt.ABNF;
using Txt.Core;

namespace Sample1.term
{
    public class TermParser : Parser<Term, double>
    {
        private readonly IParser<Factor, double> factorParser;

        public TermParser(IParser<Factor, double> factorParser)
        {
            this.factorParser = factorParser;
        }

        protected override double ParseImpl(Term term)
        {
            var left = factorParser.Parse((Factor)term[0]);
            var repetition = (Repetition)term[1];
            foreach (Concatenation concatenation in repetition)
            {
                var sign = concatenation[0];
                var factor = (Factor)concatenation[1];
                var right = factorParser.Parse(factor);
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
