using System;
using System.Collections.Generic;
using Calculator.expression;
using Calculator.number;
using Txt.ABNF;
using Txt.Core;

namespace Calculator.factor
{
    public sealed class FactorLexer : RuleLexer<Factor>
    {
        public FactorLexer(ILexer<Number> number, ILexer<Expression> expression)
        {
            if (number == null)
            {
                throw new ArgumentNullException(nameof(number));
            }
            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }
            InnerLexer = Concatenation.Create(
                Option.Create(Terminal.Create("-", StringComparer.Ordinal)),
                Alternation.Create(
                    number,
                    Concatenation.Create(
                        Terminal.Create("(", StringComparer.Ordinal),
                        expression,
                        Terminal.Create(")", StringComparer.Ordinal))));
        }

        public ILexer<Concatenation> InnerLexer { get; }

        protected override IEnumerable<Factor> ReadImpl(ITextScanner scanner, ITextContext context)
        {
            foreach (var concatenation in InnerLexer.Read(scanner, context))
            {
                yield return new Factor(concatenation);
            }
        }
    }
}
