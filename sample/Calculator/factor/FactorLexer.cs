using System;
using System.Collections.Generic;
using Txt.ABNF;
using Txt.Core;

namespace Calculator.factor
{
    public sealed class FactorLexer : Lexer<Factor>
    {
        public FactorLexer(ILexer<Concatenation> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            InnerLexer = innerLexer;
        }

        public ILexer<Concatenation> InnerLexer { get; }

        protected override IEnumerable<Factor> ReadImpl(
            ITextScanner scanner,
            ITextContext context)
        {
            foreach (var concatenation in InnerLexer.Read(scanner, context))
            {
                yield return new Factor(concatenation);
            }
        }
    }
}
