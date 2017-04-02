using System;
using System.Collections.Generic;
using Txt.ABNF;
using Txt.ABNF.Core.DIGIT;
using Txt.Core;

namespace Calculator.number
{
    public sealed class NumberLexer : RuleLexer<Number>
    {
        public NumberLexer(ILexer<Digit> digit)
        {
            if (digit == null)
            {
                throw new ArgumentNullException(nameof(digit));
            }
            var digits = Repetition.Create(digit, 1, int.MaxValue);
            var fraction = Concatenation.Create(Terminal.Create(@".", StringComparer.Ordinal), digits);
            InnerLexer = Alternation.Create(fraction, Concatenation.Create(digits, Option.Create(fraction)));
        }

        public ILexer<Alternation> InnerLexer { get; }

        protected override IEnumerable<Number> ReadImpl(ITextScanner scanner, ITextContext context)
        {
            foreach (var alternation in InnerLexer.Read(scanner, context))
            {
                yield return new Number(alternation);
            }
        }
    }
}
