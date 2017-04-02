using System;
using Calculator.expression;
using Calculator.number;
using Txt.Core;

namespace Calculator.factor
{
    public sealed class FactorLexerFactory : LexerFactory<Factor>
    {
        static FactorLexerFactory()
        {
            Default = new FactorLexerFactory(NumberLexerFactory.Default.Singleton());
        }

        public FactorLexerFactory(
            ILexerFactory<Number> numberLexerFactory)
        {
            if (numberLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(numberLexerFactory));
            }
            Number = numberLexerFactory;
            Expression = new ProxyLexer<Expression>();
        }

        public static FactorLexerFactory Default { get; }

        public ProxyLexer<Expression> Expression { get; }

        public ILexerFactory<Number> Number { get; }

        public override ILexer<Factor> Create()
        {
            return new FactorLexer(Number.Create(), Expression);
        }
    }
}
