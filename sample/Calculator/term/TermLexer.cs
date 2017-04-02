using System;
using System.Collections.Generic;
using Calculator.factor;
using Txt.ABNF;
using Txt.Core;

namespace Calculator.term
{
    public sealed class TermLexer : RuleLexer<Term>
    {
        public TermLexer(ILexer<Factor> factor)
        {
            if (factor == null)
            {
                throw new ArgumentNullException(nameof(factor));
            }
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

        public ILexer<Concatenation> InnerLexer { get; }

        protected override IEnumerable<Term> ReadImpl(ITextScanner scanner, ITextContext context)
        {
            foreach (var concatenation in InnerLexer.Read(scanner, context))
            {
                yield return new Term(concatenation);
            }
        }
    }
}
