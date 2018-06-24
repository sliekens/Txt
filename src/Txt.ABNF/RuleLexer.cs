﻿using System;
using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF
{
    public abstract class RuleLexer<TRule> : Lexer<TRule>
        where TRule : Element
    {
        protected RuleLexer([NotNull] Grammar grammar)
            : this(
                grammar,
                TerminalLexerFactory.Default,
                ValueRangeLexerFactory.Default,
                AlternationLexerFactory.Default,
                ConcatenationLexerFactory.Default,
                RepetitionLexerFactory.Default,
                OptionLexerFactory.Default)
        {
            if (grammar == null)
            {
                throw new ArgumentNullException(nameof(grammar));
            }
            Grammar = grammar;
        }

        protected RuleLexer(
            [NotNull] Grammar grammar,
            [NotNull] ITerminalLexerFactory terminalLexerFactory,
            [NotNull] IValueRangeLexerFactory valueRangeLexerFactory,
            [NotNull] IAlternationLexerFactory alternationLexerFactory,
            [NotNull] IConcatenationLexerFactory concatenationLexerFactory,
            [NotNull] IRepetitionLexerFactory repetitionLexerFactory,
            [NotNull] IOptionLexerFactory optionLexerFactory)
        {
            if (grammar == null)
            {
                throw new ArgumentNullException(nameof(grammar));
            }
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }
            if (valueRangeLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(valueRangeLexerFactory));
            }
            if (alternationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alternationLexerFactory));
            }
            if (concatenationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(concatenationLexerFactory));
            }
            if (repetitionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(repetitionLexerFactory));
            }
            if (optionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(optionLexerFactory));
            }
            Grammar = grammar;
            Terminal = terminalLexerFactory;
            ValueRange = valueRangeLexerFactory;
            Alternation = alternationLexerFactory;
            Concatenation = concatenationLexerFactory;
            Repetition = repetitionLexerFactory;
            Option = optionLexerFactory;
        }

        [NotNull]
        public IAlternationLexerFactory Alternation { get; set; }

        [NotNull]
        public IConcatenationLexerFactory Concatenation { get; set; }

        [NotNull]
        public IOptionLexerFactory Option { get; set; }

        [NotNull]
        public IRepetitionLexerFactory Repetition { get; set; }

        [NotNull]
        public ITerminalLexerFactory Terminal { get; set; }

        [NotNull]
        public IValueRangeLexerFactory ValueRange { get; set; }

        protected Grammar Grammar { get; }
    }
}