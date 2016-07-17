using System;
using Calculator.expression;
using Calculator.number;
using Txt.ABNF;
using Txt.Core;

namespace Calculator.factor
{
    public class FactorLexerFactory : ILexerFactory<Factor>
    {
        private readonly IAlternationLexerFactory alternationLexerFactory;

        private readonly IConcatenationLexerFactory concatenationLexerFactory;

        private readonly ILexer<Expression> expressionLexer;

        private readonly ILexer<Number> numberLexer;

        private readonly IOptionLexerFactory optionLexerFactory;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        public FactorLexerFactory(
            IConcatenationLexerFactory concatenationLexerFactory,
            IOptionLexerFactory optionLexerFactory,
            ITerminalLexerFactory terminalLexerFactory,
            IAlternationLexerFactory alternationLexerFactory,
            ILexer<Number> numberLexer,
            ILexer<Expression> expressionLexer)
        {
            this.concatenationLexerFactory = concatenationLexerFactory;
            this.optionLexerFactory = optionLexerFactory;
            this.terminalLexerFactory = terminalLexerFactory;
            this.alternationLexerFactory = alternationLexerFactory;
            this.numberLexer = numberLexer;
            this.expressionLexer = expressionLexer;
        }

        public ILexer<Factor> Create()
        {
            return new FactorLexer(
                concatenationLexerFactory.Create(
                    optionLexerFactory.Create(
                        terminalLexerFactory.Create("-", StringComparer.Ordinal)),
                    alternationLexerFactory.Create(
                        numberLexer,
                        concatenationLexerFactory.Create(
                            terminalLexerFactory.Create("(", StringComparer.Ordinal),
                            expressionLexer,
                            terminalLexerFactory.Create(")", StringComparer.Ordinal)))));
        }
    }
}
