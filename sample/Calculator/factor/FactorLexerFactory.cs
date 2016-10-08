using System;
using Calculator.expression;
using Calculator.number;
using Txt.ABNF;
using Txt.Core;

namespace Calculator.factor
{
    public class FactorLexerFactory : LexerFactory<Factor>
    {
        static FactorLexerFactory()
        {
            Default = new FactorLexerFactory(
                Txt.ABNF.ConcatenationLexerFactory.Default,
                Txt.ABNF.OptionLexerFactory.Default,
                Txt.ABNF.TerminalLexerFactory.Default,
                Txt.ABNF.AlternationLexerFactory.Default,
                number.NumberLexerFactory.Default.Singleton(),
                new ProxyLexer<Expression>());
        }

        public FactorLexerFactory(
            IConcatenationLexerFactory concatenationLexerFactory,
            IOptionLexerFactory optionLexerFactory,
            ITerminalLexerFactory terminalLexerFactory,
            IAlternationLexerFactory alternationLexerFactory,
            ILexerFactory<Number> numberLexerFactory,
            ProxyLexer<Expression> expressionProxyLexer)
        {
            ConcatenationLexerFactory = concatenationLexerFactory;
            OptionLexerFactory = optionLexerFactory;
            TerminalLexerFactory = terminalLexerFactory;
            AlternationLexerFactory = alternationLexerFactory;
            NumberLexerFactory = numberLexerFactory;
            ExpressionProxyLexer = expressionProxyLexer;
        }

        public static FactorLexerFactory Default { get; }

        public IAlternationLexerFactory AlternationLexerFactory { get; }

        public IConcatenationLexerFactory ConcatenationLexerFactory { get; }

        public ProxyLexer<Expression> ExpressionProxyLexer { get; }

        public ILexerFactory<Number> NumberLexerFactory { get; }

        public IOptionLexerFactory OptionLexerFactory { get; }

        public ITerminalLexerFactory TerminalLexerFactory { get; }

        public override ILexer<Factor> Create()
        {
            var numberLexer = NumberLexerFactory.Create();
            return new FactorLexer(
                ConcatenationLexerFactory.Create(
                    OptionLexerFactory.Create(
                        TerminalLexerFactory.Create("-", StringComparer.Ordinal)),
                    AlternationLexerFactory.Create(
                        numberLexer,
                        ConcatenationLexerFactory.Create(
                            TerminalLexerFactory.Create("(", StringComparer.Ordinal),
                            ExpressionProxyLexer,
                            TerminalLexerFactory.Create(")", StringComparer.Ordinal)))));
        }
    }
}
