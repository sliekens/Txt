using System;
using Calculator.factor;
using Txt.ABNF;
using Txt.Core;

namespace Calculator.term
{
    public sealed class TermLexerFactory : RuleLexerFactory<Term>
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
            var factor = Factor.Create();
            var innerLexer = Concatenation.Create(
                factor,
                Repetition.Create(
                    Concatenation.Create(
                        Alternation.Create(
                            Terminal.Create(@"*", StringComparer.Ordinal),
                            Terminal.Create(@"/", StringComparer.Ordinal)),
                        factor),
                    0,
                    int.MaxValue));
            return new TermLexer(innerLexer);
        }
    }
}
