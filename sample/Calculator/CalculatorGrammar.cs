using Calculator.expression;
using Calculator.factor;
using Calculator.number;
using Calculator.term;
using Txt.ABNF;

namespace Calculator
{
    public class CalculatorGrammar : CoreGrammar
    {
        public CalculatorGrammar()
        {
            AddRule("expression", new ExpressionLexer(this));
            AddRule("term", new TermLexer(this));
            AddRule("factor", new FactorLexer(this));
            AddRule("number", new NumberLexer(this));
        }
    }
}
