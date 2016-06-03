using Sample1.factor;
using Txt.ABNF;
using Txt.Core;

namespace Sample1.term
{
    public class TermParser : Parser<Term, decimal>
    {
        private readonly IParser<Factor, decimal> factorParser;

        public TermParser(IParser<Factor, decimal> factorParser)
        {
            this.factorParser = factorParser;
        }

        protected override decimal ParseImpl(Term term)
        {
            var value = factorParser.Parse((Factor)term[0]);
            var repetition = (Repetition)term[1];
            foreach (Concatenation concatenation in repetition)
            {
                var sign = concatenation[0];
                var factor = (Factor)concatenation[1];
                if (sign.Text == "*")
                {
                    value *= factorParser.Parse(factor);
                }
                else if (sign.Text == "/")
                {
                    value /= factorParser.Parse(factor);
                }
            }
            return value;
        }
    }
}
