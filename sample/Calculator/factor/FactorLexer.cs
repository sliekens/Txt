using System;
using System.Collections.Generic;
using Txt.ABNF;
using Txt.Core;

namespace Calculator.factor
{
    public sealed class FactorLexer : RuleLexer<Factor>, IInitializable
    {
        public FactorLexer(Grammar grammar)
            : base(grammar)
        {
        }

        public ILexer<Element> InnerLexer { get; private set; }

        public void Initialize()
        {
            var number = Grammar.Rule("number");
            var expression = Grammar.Rule("expression");
            InnerLexer = Concatenation.Create(
                Option.Create(Terminal.Create("-", StringComparer.Ordinal)),
                Alternation.Create(
                    number,
                    Concatenation.Create(
                        Terminal.Create("(", StringComparer.Ordinal),
                        expression,
                        Terminal.Create(")", StringComparer.Ordinal))));
        }

        protected override IEnumerable<Factor> ReadImpl(ITextScanner scanner, ITextContext context)
        {
            foreach (var concatenation in InnerLexer.Read(scanner, context))
            {
                yield return new Factor(concatenation);
            }
        }
    }
}
