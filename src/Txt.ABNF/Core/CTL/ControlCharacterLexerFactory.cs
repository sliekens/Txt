using System;
using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF.Core.CTL
{
    /// <summary>Creates instances of the <see cref="ControlCharacterLexer" /> class.</summary>
    public class ControlCharacterLexerFactory : LexerFactory<ControlCharacter>
    {
        static ControlCharacterLexerFactory()
        {
            Default = new ControlCharacterLexerFactory(
                ABNF.ValueRangeLexerFactory.Default,
                ABNF.TerminalLexerFactory.Default,
                ABNF.AlternationLexerFactory.Default);
        }

        /// <summary>
        /// </summary>
        /// <param name="valueRangeLexerFactory"></param>
        /// <param name="terminalLexerFactory"></param>
        /// <param name="alternationLexerFactory"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public ControlCharacterLexerFactory(
            [NotNull] IValueRangeLexerFactory valueRangeLexerFactory,
            [NotNull] ITerminalLexerFactory terminalLexerFactory,
            [NotNull] IAlternationLexerFactory alternationLexerFactory)
        {
            if (valueRangeLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(valueRangeLexerFactory));
            }
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }
            if (alternationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alternationLexerFactory));
            }
            ValueRangeLexerFactory = valueRangeLexerFactory;
            TerminalLexerFactory = terminalLexerFactory;
            AlternationLexerFactory = alternationLexerFactory;
        }

        [NotNull]
        public static ControlCharacterLexerFactory Default { get; }

        [NotNull]
        public IAlternationLexerFactory AlternationLexerFactory { get; }

        [NotNull]
        public ITerminalLexerFactory TerminalLexerFactory { get; }

        [NotNull]
        public IValueRangeLexerFactory ValueRangeLexerFactory { get; }

        /// <inheritdoc />
        public override ILexer<ControlCharacter> Create()
        {
            var controlsValueRange = ValueRangeLexerFactory.Create('\x00', '\x1F');
            var delete = TerminalLexerFactory.Create("\x7F", StringComparer.Ordinal);
            var innerLexer = AlternationLexerFactory.Create(controlsValueRange, delete);
            return new ControlCharacterLexer(innerLexer);
        }

        [NotNull]
        public ControlCharacterLexerFactory UseAlternationLexerFactory(
            [NotNull] IAlternationLexerFactory alternationLexerFactory)
        {
            if (alternationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alternationLexerFactory));
            }
            return new ControlCharacterLexerFactory(
                ValueRangeLexerFactory,
                TerminalLexerFactory,
                alternationLexerFactory);
        }

        [NotNull]
        public ControlCharacterLexerFactory UseTerminalLexerFactory(
            [NotNull] ITerminalLexerFactory terminalLexerFactory)
        {
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }
            return new ControlCharacterLexerFactory(
                ValueRangeLexerFactory,
                terminalLexerFactory,
                AlternationLexerFactory);
        }

        [NotNull]
        public ControlCharacterLexerFactory UseValueRangeLexerFactory(
            [NotNull] IValueRangeLexerFactory valueRangeLexerFactory)
        {
            if (valueRangeLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(valueRangeLexerFactory));
            }
            return new ControlCharacterLexerFactory(
                valueRangeLexerFactory,
                TerminalLexerFactory,
                AlternationLexerFactory);
        }
    }
}
