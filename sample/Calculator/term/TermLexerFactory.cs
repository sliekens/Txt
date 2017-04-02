using System;
using Calculator.factor;
using Txt.Core;

namespace Calculator.term
{
    public sealed class TermLexerFactory : LexerFactory<Term>
    {
        static TermLexerFactory()
        {
            Default = new TermLexerFactory(FactorLexerFactory.Default.Singleton());
        }

        public TermLexerFactory(ILexerFactory<Factor> factorLexerFactory)
        {
            if (factorLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(factorLexerFactory));
            }
            Factor = factorLexerFactory;
        }

        public static TermLexerFactory Default { get; }

        public ILexerFactory<Factor> Factor { get; }

        public override ILexer<Term> Create()
        {
            return new TermLexer(Factor.Create());
        }
    }
}
