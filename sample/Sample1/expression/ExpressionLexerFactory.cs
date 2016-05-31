using System;
using Sample1.term;
using Txt.ABNF;
using Txt.Core;

namespace Sample1.expression
{
    public class ExpressionLexerFactory : ILexerFactory<Expression>
    {
        private readonly IConcatenationLexerFactory concatenationLexerFactory;

        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        private readonly IAlternationLexerFactory alternationLexerFactory;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        private readonly ILexer<Term> termLexer;

        public ExpressionLexerFactory(
            IConcatenationLexerFactory concatenationLexerFactory,
            IRepetitionLexerFactory repetitionLexerFactory,
            IAlternationLexerFactory alternationLexerFactory,
            ITerminalLexerFactory terminalLexerFactory,
            ILexer<Term> termLexer)
        {
            this.concatenationLexerFactory = concatenationLexerFactory;
            this.repetitionLexerFactory = repetitionLexerFactory;
            this.alternationLexerFactory = alternationLexerFactory;
            this.terminalLexerFactory = terminalLexerFactory;
            this.termLexer = termLexer;
        }

        public ILexer<Expression> Create()
        {
            return new ExpressionLexer(
                concatenationLexerFactory.Create(
                    termLexer,
                    repetitionLexerFactory.Create(
                        concatenationLexerFactory.Create(
                            alternationLexerFactory.Create(
                            terminalLexerFactory.Create("+", StringComparer.Ordinal),
                            terminalLexerFactory.Create("-", StringComparer.Ordinal)),
                            termLexer),
                        0,
                        int.MaxValue)));
        }
    }
}
