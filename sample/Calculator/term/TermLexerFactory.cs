using System;
using Calculator.factor;
using Txt.ABNF;
using Txt.Core;

namespace Calculator.term
{
    public class TermLexerFactory : ILexerFactory<Term>
    {
        private ILexer<Term> instance;

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

        public IAlternationLexerFactory AlternationLexerFactory { get; }

        public IConcatenationLexerFactory ConcatenationLexerFactory { get; }

        public ILexerFactory<Factor> FactorLexerFactory { get; }

        public IRepetitionLexerFactory RepetitionLexerFactory { get; }

        public ITerminalLexerFactory TerminalLexerFactory { get; }

        public ILexer<Term> Create()
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
