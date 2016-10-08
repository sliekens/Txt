using System;
using Calculator.term;
using Txt.ABNF;
using Txt.Core;

namespace Calculator.expression
{
    public class ExpressionLexerFactory : LexerFactory<Expression>
    {
        static ExpressionLexerFactory()
        {
            Default = new ExpressionLexerFactory(
                Txt.ABNF.ConcatenationLexerFactory.Default,
                Txt.ABNF.RepetitionLexerFactory.Default,
                Txt.ABNF.AlternationLexerFactory.Default,
                Txt.ABNF.TerminalLexerFactory.Default,
                term.TermLexerFactory.Default.Singleton());
        }

        public ExpressionLexerFactory(
            IConcatenationLexerFactory concatenationLexerFactory,
            IRepetitionLexerFactory repetitionLexerFactory,
            IAlternationLexerFactory alternationLexerFactory,
            ITerminalLexerFactory terminalLexerFactory,
            ILexerFactory<Term> termLexerFactory)
        {
            ConcatenationLexerFactory = concatenationLexerFactory;
            RepetitionLexerFactory = repetitionLexerFactory;
            AlternationLexerFactory = alternationLexerFactory;
            TerminalLexerFactory = terminalLexerFactory;
            TermLexerFactory = termLexerFactory;
        }

        public static ExpressionLexerFactory Default { get; }

        public IAlternationLexerFactory AlternationLexerFactory { get; }

        public IConcatenationLexerFactory ConcatenationLexerFactory { get; }

        public IRepetitionLexerFactory RepetitionLexerFactory { get; }

        public ITerminalLexerFactory TerminalLexerFactory { get; }

        public ILexerFactory<Term> TermLexerFactory { get; }

        public override ILexer<Expression> Create()
        {
            var termLexer = TermLexerFactory.Create();
            return new ExpressionLexer(
                ConcatenationLexerFactory.Create(
                    termLexer,
                    RepetitionLexerFactory.Create(
                        ConcatenationLexerFactory.Create(
                            AlternationLexerFactory.Create(
                                TerminalLexerFactory.Create("+", StringComparer.Ordinal),
                                TerminalLexerFactory.Create("-", StringComparer.Ordinal)),
                            termLexer),
                        0,
                        int.MaxValue)));
        }
    }
}
