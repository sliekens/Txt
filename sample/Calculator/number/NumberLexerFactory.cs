using System;
using Txt.ABNF;
using Txt.ABNF.Core.DIGIT;
using Txt.Core;

namespace Calculator.number
{
    public class NumberLexerFactory : ILexerFactory<Number>
    {
        private readonly IAlternationLexerFactory alternationLexerFactory;

        private readonly IConcatenationLexerFactory concatenationLexerFactory;

        private readonly ILexer<Digit> digitLexer;

        private readonly IOptionLexerFactory optionLexerFactory;

        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        public NumberLexerFactory(
            IAlternationLexerFactory alternationLexerFactory,
            IConcatenationLexerFactory concatenationLexerFactory,
            IRepetitionLexerFactory repetitionLexerFactory,
            IOptionLexerFactory optionLexerFactory,
            ITerminalLexerFactory terminalLexerFactory,
            ILexer<Digit> digitLexer)
        {
            this.alternationLexerFactory = alternationLexerFactory;
            this.concatenationLexerFactory = concatenationLexerFactory;
            this.repetitionLexerFactory = repetitionLexerFactory;
            this.optionLexerFactory = optionLexerFactory;
            this.terminalLexerFactory = terminalLexerFactory;
            this.digitLexer = digitLexer;
        }

        public ILexer<Number> Create()
        {
            var digits = repetitionLexerFactory.Create(digitLexer, 1, int.MaxValue);
            var fraction =
                concatenationLexerFactory.Create(
                    terminalLexerFactory.Create(@".", StringComparer.OrdinalIgnoreCase),
                    digits);
            var innerLexer = alternationLexerFactory.Create(
                fraction,
                concatenationLexerFactory.Create(digits, optionLexerFactory.Create(fraction)));
            return new NumberLexer(innerLexer);
        }
    }
}
