using System;
using Calculator.expression;
using Calculator.number;
using Txt.ABNF;
using Txt.Core;

namespace Calculator.factor
{
    public sealed class FactorLexerFactory : RuleLexerFactory<Factor>
    {
        static FactorLexerFactory()
        {
            Default = new FactorLexerFactory(
                NumberLexerFactory.Default.Singleton(),
                new ProxyLexer<Expression>());
        }

        public FactorLexerFactory(
            ILexerFactory<Number> numberLexerFactory,
            ProxyLexer<Expression> expressionLexer)
        {
            if (numberLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(numberLexerFactory));
            }
            if (expressionLexer == null)
            {
                throw new ArgumentNullException(nameof(expressionLexer));
            }
            Number = numberLexerFactory;
            Expression = expressionLexer;
        }

        public static FactorLexerFactory Default { get; }

        public ProxyLexer<Expression> Expression { get; }

        public ILexerFactory<Number> Number { get; }

        public override ILexer<Factor> Create()
        {
            var number = Number.Create();
            var innerLexer = Concatenation.Create(
                Option.Create(Terminal.Create("-", StringComparer.Ordinal)),
                Alternation.Create(
                    number,
                    Concatenation.Create(
                        Terminal.Create("(", StringComparer.Ordinal),
                        Expression,
                        Terminal.Create(")", StringComparer.Ordinal))));
            return new FactorLexer(innerLexer);
        }
    }
}
