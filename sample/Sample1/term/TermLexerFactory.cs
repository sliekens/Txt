using System;
using Sample1.factor;
using Txt.ABNF;
using Txt.Core;

namespace Sample1.term
{
    public class TermLexerFactory : ILexerFactory<Term>
    {
        private readonly IConcatenationLexerFactory concatenationLexerFactory;

        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        private readonly IAlternationLexerFactory alternationLexerFactory;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        private readonly ILexer<Factor> factorLexer;

        public TermLexerFactory(
            IConcatenationLexerFactory concatenationLexerFactory,
            IRepetitionLexerFactory repetitionLexerFactory,
            IAlternationLexerFactory alternationLexerFactory,
            ITerminalLexerFactory terminalLexerFactory,
            ILexer<Factor> factorLexer)
        {
            this.concatenationLexerFactory = concatenationLexerFactory;
            this.repetitionLexerFactory = repetitionLexerFactory;
            this.alternationLexerFactory = alternationLexerFactory;
            this.terminalLexerFactory = terminalLexerFactory;
            this.factorLexer = factorLexer;
        }

        public ILexer<Term> Create()
        {
            return new TermLexer(
                concatenationLexerFactory.Create(
                    factorLexer,
                    repetitionLexerFactory.Create(
                        concatenationLexerFactory.Create(
                            alternationLexerFactory.Create(
                                terminalLexerFactory.Create("*", StringComparer.Ordinal),
                                terminalLexerFactory.Create("/", StringComparer.Ordinal)),
                            factorLexer),
                        0,
                        int.MaxValue)));
        }
    }
}
