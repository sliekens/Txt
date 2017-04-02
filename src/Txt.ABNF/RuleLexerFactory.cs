﻿using System;
using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF
{
    [Obsolete("Rule factories have moved to RuleLexer. Change lexers to derive from RuleLexer and build up rules in their constructor.", true)]
    public abstract class RuleLexerFactory<T> : LexerFactory<T>
        where T : Element
    {
        protected RuleLexerFactory()
            : this(
                TerminalLexerFactory.Default,
                ValueRangeLexerFactory.Default,
                AlternationLexerFactory.Default,
                ConcatenationLexerFactory.Default,
                RepetitionLexerFactory.Default,
                OptionLexerFactory.Default)
        {
        }

        protected RuleLexerFactory(
            [NotNull] ITerminalLexerFactory terminalLexerFactory,
            [NotNull] IValueRangeLexerFactory valueRangeLexerFactory,
            [NotNull] IAlternationLexerFactory alternationLexerFactory,
            [NotNull] IConcatenationLexerFactory concatenationLexerFactory,
            [NotNull] IRepetitionLexerFactory repetitionLexerFactory,
            [NotNull] IOptionLexerFactory optionLexerFactory)
        {
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
    }
}
