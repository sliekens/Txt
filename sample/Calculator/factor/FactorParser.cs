using Calculator.expression;
using Calculator.number;
using Txt.ABNF;
using Txt.Core;

namespace Calculator.factor
{
    public class FactorParser : Parser<Factor, double>
    {
        private readonly IParser<Expression, double> expressionParser;

        private readonly IParser<Number, double> numberParser;

        public FactorParser(IParser<Number, double> numberParser, IParser<Expression, double> expressionParser)
        {
            this.numberParser = numberParser;
            this.expressionParser = expressionParser;
        }

        protected override double ParseImpl(Factor factor)
        {
            double value = 0;
            var alt = factor[1] as Alternation;
            if (alt.Ordinal == 1)
            {
                var number = (Number)alt.Element;
                value = numberParser.Parse(number);
            }
            if (alt.Ordinal == 2)
            {
                var concatenation = (Concatenation)alt.Element;
                var expression = (Expression)concatenation[1];
                value = expressionParser.Parse(expression);
            }
            if (factor[0].Text == "-")
            {
                value *= -1;
            }
            return value;
        }
    }
}
