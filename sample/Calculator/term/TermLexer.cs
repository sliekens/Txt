using System;
using System.Collections.Generic;
using Txt.ABNF;
using Txt.Core;

namespace Calculator.term
{
    public sealed class TermLexer : Lexer<Term>
    {
        public TermLexer(ILexer<Concatenation> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            InnerLexer = innerLexer;
        }

        public ILexer<Concatenation> InnerLexer { get; }

        protected override IEnumerable<Term> ReadImpl(
            ITextScanner scanner,
            ITextContext context)
        {
            foreach (var concatenation in InnerLexer.Read(scanner, context))
            {
                yield return new Term(concatenation);
            }
        }
    }
}
