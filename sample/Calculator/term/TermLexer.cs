using System;
using System.Collections.Generic;
using Txt.ABNF;
using Txt.Core;

namespace Calculator.term
{
    public sealed class TermLexer : RuleLexer<Term>, IInitializable
    {
        public TermLexer(Grammar grammar)
            : base(grammar)
        {
        }

        public ILexer<Element> InnerLexer { get; private set; }

        public void Initialize()
        {
            var factor = Grammar.Rule("factor");
            InnerLexer = Concatenation.Create(
                factor,
                Repetition.Create(
                    Concatenation.Create(
                        Alternation.Create(
                            Terminal.Create(@"*", StringComparer.Ordinal),
                            Terminal.Create(@"/", StringComparer.Ordinal)),
                        factor),
                    0,
                    int.MaxValue));
        }

        protected override IEnumerable<Term> ReadImpl(ITextScanner scanner, ITextContext context)
        {
            foreach (var concatenation in InnerLexer.Read(scanner, context))
            {
                yield return new Term(concatenation);
            }
        }
    }
}
