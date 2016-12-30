using System;
using System.Collections.Generic;
using Txt.ABNF;
using Txt.Core;

namespace Calculator.expression
{
    public sealed class ExpressionLexer : Lexer<Expression>
    {
        public ExpressionLexer(ILexer<Concatenation> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            InnerLexer = innerLexer;
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
