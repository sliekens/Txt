using System;
using Calculator.expression;
using Calculator.number;
using Txt.ABNF;
using Txt.Core;

namespace Calculator.factor
{
    public class FactorLexerFactory : ILexerFactory<Factor>
    {
        private ILexer<Factor> instance;

        public FactorLexerFactory(
            IConcatenationLexerFactory concatenationLexerFactory,
            IOptionLexerFactory optionLexerFactory,
            ITerminalLexerFactory terminalLexerFactory,
            IAlternationLexerFactory alternationLexerFactory,
            ILexerFactory<Number> numberLexerFactory,
            ILexerFactory<Expression> expressionLexerFactory)
        {
            ConcatenationLexerFactory = concatenationLexerFactory;
            OptionLexerFactory = optionLexerFactory;
            TerminalLexerFactory = terminalLexerFactory;
            AlternationLexerFactory = alternationLexerFactory;
            NumberLexerFactory = numberLexerFactory;
            ExpressionLexerFactory = expressionLexerFactory;
        }

        public IAlternationLexerFactory AlternationLexerFactory { get; }

        public IConcatenationLexerFactory ConcatenationLexerFactory { get; }

        public ILexerFactory<Expression> ExpressionLexerFactory { get; }

        public ILexerFactory<Number> NumberLexerFactory { get; }

        public IOptionLexerFactory OptionLexerFactory { get; }

        public ITerminalLexerFactory TerminalLexerFactory { get; }

        public ILexer<Factor> Create()
        {
            var numberLexer = NumberLexerFactory.CreateOnce();
            var expressionLexer = ExpressionLexerFactory.CreateOnce();
            return new FactorLexer(
                ConcatenationLexerFactory.Create(
                    OptionLexerFactory.Create(
                        TerminalLexerFactory.Create("-", StringComparer.Ordinal)),
                    AlternationLexerFactory.Create(
                        numberLexer,
                        ConcatenationLexerFactory.Create(
                            TerminalLexerFactory.Create("(", StringComparer.Ordinal),
                            expressionLexer,
                            TerminalLexerFactory.Create(")", StringComparer.Ordinal)))));
        }

        public ILexer<Factor> CreateOnce()
        {
            return instance ?? (instance = Create());
        }
    }
}
