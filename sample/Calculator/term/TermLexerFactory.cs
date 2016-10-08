using System;
using Calculator.factor;
using Txt.ABNF;
using Txt.Core;

namespace Calculator.term
{
    public class TermLexerFactory : LexerFactory<Term>
    {
        static TermLexerFactory()
        {
            Default = new TermLexerFactory(
                Txt.ABNF.ConcatenationLexerFactory.Default,
                Txt.ABNF.RepetitionLexerFactory.Default,
                Txt.ABNF.AlternationLexerFactory.Default,
                Txt.ABNF.TerminalLexerFactory.Default,
                factor.FactorLexerFactory.Default.Singleton());
        }

        public TermLexerFactory(
            IConcatenationLexerFactory concatenationLexerFactory,
            IRepetitionLexerFactory repetitionLexerFactory,
            IAlternationLexerFactory alternationLexerFactory,
            ITerminalLexerFactory terminalLexerFactory,
            ILexerFactory<Factor> factorLexerFactory)
        {
            ConcatenationLexerFactory = concatenationLexerFactory;
            RepetitionLexerFactory = repetitionLexerFactory;
            AlternationLexerFactory = alternationLexerFactory;
            TerminalLexerFactory = terminalLexerFactory;
            FactorLexerFactory = factorLexerFactory;
        }

        public static TermLexerFactory Default { get; }

        public IAlternationLexerFactory AlternationLexerFactory { get; }

        public IConcatenationLexerFactory ConcatenationLexerFactory { get; }

        public ILexerFactory<Factor> FactorLexerFactory { get; }

        public IRepetitionLexerFactory RepetitionLexerFactory { get; }

        public ITerminalLexerFactory TerminalLexerFactory { get; }

        public override ILexer<Term> Create()
        {
            var factorLexer = FactorLexerFactory.Create();
            return new TermLexer(
                ConcatenationLexerFactory.Create(
                    factorLexer,
                    RepetitionLexerFactory.Create(
                        ConcatenationLexerFactory.Create(
                            AlternationLexerFactory.Create(
                                TerminalLexerFactory.Create("*", StringComparer.Ordinal),
                                TerminalLexerFactory.Create("/", StringComparer.Ordinal)),
                            factorLexer),
                        0,
                        int.MaxValue)));
        }
    }
}
