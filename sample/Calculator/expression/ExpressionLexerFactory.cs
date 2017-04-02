using System;
using Calculator.term;
using Txt.Core;

namespace Calculator.expression
{
    public sealed class ExpressionLexerFactory : LexerFactory<Expression>
    {
        static ExpressionLexerFactory()
        {
            Default = new ExpressionLexerFactory(TermLexerFactory.Default.Singleton());
        }

        public ExpressionLexerFactory(ILexerFactory<Term> termLexerFactory)
        {
            if (termLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(termLexerFactory));
            }
            Term = termLexerFactory;
        }

        public static ExpressionLexerFactory Default { get; }

        public ILexerFactory<Term> Term { get; }

        public override ILexer<Expression> Create()
        {
            return new ExpressionLexer(Term.Create());
        }
    }
}
