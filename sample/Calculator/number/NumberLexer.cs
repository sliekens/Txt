using System;
using System.Collections.Generic;
using Txt.ABNF;
using Txt.Core;

namespace Calculator.number
{
    public sealed class NumberLexer : RuleLexer<Number>, IInitializable
    {
        public NumberLexer(Grammar grammar)
            : base(grammar)
        {
        }

        public ILexer<Element> InnerLexer { get; private set; }

        public void Initialize()
        {
            var digit = Grammar.Rule("DIGIT");
            var digits = Repetition.Create(digit, 1, int.MaxValue);
            var fraction = Concatenation.Create(Terminal.Create(@".", StringComparer.Ordinal), digits);
            InnerLexer = Alternation.Create(fraction, Concatenation.Create(digits, Option.Create(fraction)));
        }

        protected override IEnumerable<Number> ReadImpl(ITextScanner scanner, ITextContext context)
        {
            foreach (var alternation in InnerLexer.Read(scanner, context))
            {
                yield return new Number(alternation);
            }
        }
    }
}
