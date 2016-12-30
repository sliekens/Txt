using System;
using System.Collections.Generic;
using Txt.ABNF;
using Txt.Core;

namespace Calculator.number
{
    public sealed class NumberLexer : Lexer<Number>
    {
        public NumberLexer(ILexer<Alternation> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            InnerLexer = innerLexer;
        }

        public ILexer<Alternation> InnerLexer { get; }

        protected override IEnumerable<Number> ReadImpl(
            ITextScanner scanner,
            ITextContext context)
        {
            foreach (var alternation in InnerLexer.Read(scanner, context))
            {
                yield return new Number(alternation);
            }
        }
    }
}
