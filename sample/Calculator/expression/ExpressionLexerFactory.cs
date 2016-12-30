using System;
using Calculator.term;
using Txt.ABNF;
using Txt.Core;

namespace Calculator.expression
{
    public sealed class ExpressionLexerFactory : RuleLexerFactory<Expression>
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
            var term = Term.Create();
            var innerLexer = Concatenation.Create(
                term,
                Repetition.Create(
                    Concatenation.Create(
                        Alternation.Create(
                            Terminal.Create("+", StringComparer.Ordinal),
                            Terminal.Create("-", StringComparer.Ordinal)),
                        term),
                    0,
                    int.MaxValue));
            return new ExpressionLexer(innerLexer);
        }
    }
}
