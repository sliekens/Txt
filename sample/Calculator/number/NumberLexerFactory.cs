using System;
using Txt.ABNF;
using Txt.ABNF.Core.DIGIT;
using Txt.Core;

namespace Calculator.number
{
    public class NumberLexerFactory : LexerFactory<Number>
    {
        static NumberLexerFactory()
        {
            Default = new NumberLexerFactory(
                Txt.ABNF.AlternationLexerFactory.Default,
                Txt.ABNF.ConcatenationLexerFactory.Default,
                Txt.ABNF.RepetitionLexerFactory.Default,
                Txt.ABNF.OptionLexerFactory.Default,
                Txt.ABNF.TerminalLexerFactory.Default,
                Txt.ABNF.Core.DIGIT.DigitLexerFactory.Default.Singleton());
        }

        public NumberLexerFactory(
            IAlternationLexerFactory alternationLexerFactory,
            IConcatenationLexerFactory concatenationLexerFactory,
            IRepetitionLexerFactory repetitionLexerFactory,
            IOptionLexerFactory optionLexerFactory,
            ITerminalLexerFactory terminalLexerFactory,
            ILexerFactory<Digit> digitLexerFactory)
        {
            AlternationLexerFactory = alternationLexerFactory;
            ConcatenationLexerFactory = concatenationLexerFactory;
            RepetitionLexerFactory = repetitionLexerFactory;
            OptionLexerFactory = optionLexerFactory;
            TerminalLexerFactory = terminalLexerFactory;
            DigitLexerFactory = digitLexerFactory;
        }

        public static NumberLexerFactory Default { get; }

        public IAlternationLexerFactory AlternationLexerFactory { get; }

        public IConcatenationLexerFactory ConcatenationLexerFactory { get; }

        public ILexerFactory<Digit> DigitLexerFactory { get; }

        public IOptionLexerFactory OptionLexerFactory { get; }

        public IRepetitionLexerFactory RepetitionLexerFactory { get; }

        public ITerminalLexerFactory TerminalLexerFactory { get; }

        public override ILexer<Number> Create()
        {
            var digits =
                RepetitionLexerFactory.Create(DigitLexerFactory.Create(), 1, int.MaxValue);
            var fraction =
                ConcatenationLexerFactory.Create(
                    TerminalLexerFactory.Create(@".", StringComparer.OrdinalIgnoreCase),
                    digits);
            var innerLexer =
                AlternationLexerFactory.Create(
                    fraction,
                    ConcatenationLexerFactory.Create(digits, OptionLexerFactory.Create(fraction)));
            return new NumberLexer(innerLexer);
        }
    }
}
