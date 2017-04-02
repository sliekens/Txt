using System;
using System.Collections.Generic;
using Calculator.term;
using Txt.ABNF;
using Txt.Core;

namespace Calculator.expression
{
    public sealed class ExpressionLexer : RuleLexer<Expression>
    {
        public ExpressionLexer(ILexer<Term> term)
        {
            if (term == null)
            {
                throw new ArgumentNullException(nameof(term));
            }
            InnerLexer = Concatenation.Create(
                term,
                Repetition.Create(
                    Concatenation.Create(
                        Alternation.Create(
                            Terminal.Create("+", StringComparer.Ordinal),
                            Terminal.Create("-", StringComparer.Ordinal)),
                        term),
                    0,
                    int.MaxValue));
        }

        public ILexer<Concatenation> InnerLexer { get; }

        protected override IEnumerable<Expression> ReadImpl(
            ITextScanner scanner,
            ITextContext context)
        {
            foreach (var concatenation in InnerLexer.Read(scanner, context))
            {
                yield return new Expression(concatenation);
            }
        }
    }
}
