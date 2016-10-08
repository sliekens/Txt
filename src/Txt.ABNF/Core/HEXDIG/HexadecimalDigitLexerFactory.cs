using System;
using JetBrains.Annotations;
using Txt.ABNF.Core.DIGIT;
using Txt.Core;

namespace Txt.ABNF.Core.HEXDIG
{
    /// <summary>Creates instances of the <see cref="HexadecimalDigitLexer" /> class.</summary>
    public class HexadecimalDigitLexerFactory : LexerFactory<HexadecimalDigit>
    {
        static HexadecimalDigitLexerFactory()
        {
            Default = new HexadecimalDigitLexerFactory(
                ABNF.TerminalLexerFactory.Default,
                ABNF.AlternationLexerFactory.Default,
                DIGIT.DigitLexerFactory.Default);
        }

        public HexadecimalDigitLexerFactory(
            [NotNull] ITerminalLexerFactory terminalLexerFactory,
            [NotNull] IAlternationLexerFactory alternationLexerFactory,
            [NotNull] ILexerFactory<Digit> digitLexerFactory)
        {
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }
            if (alternationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alternationLexerFactory));
            }
            if (digitLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(digitLexerFactory));
            }
            TerminalLexerFactory = terminalLexerFactory;
            AlternationLexerFactory = alternationLexerFactory;
            DigitLexerFactory = digitLexerFactory;
        }

        [NotNull]
        public static HexadecimalDigitLexerFactory Default { get; }

        [NotNull]
        public IAlternationLexerFactory AlternationLexerFactory { get; }

        [NotNull]
        public ILexerFactory<Digit> DigitLexerFactory { get; }

        [NotNull]
        public ITerminalLexerFactory TerminalLexerFactory { get; }

        /// <inheritdoc />
        public override ILexer<HexadecimalDigit> Create()
        {
            var innerLexer = AlternationLexerFactory.Create(
                DigitLexerFactory.Create(),
                TerminalLexerFactory.Create("A", StringComparer.OrdinalIgnoreCase),
                TerminalLexerFactory.Create("B", StringComparer.OrdinalIgnoreCase),
                TerminalLexerFactory.Create("C", StringComparer.OrdinalIgnoreCase),
                TerminalLexerFactory.Create("D", StringComparer.OrdinalIgnoreCase),
                TerminalLexerFactory.Create("E", StringComparer.OrdinalIgnoreCase),
                TerminalLexerFactory.Create("F", StringComparer.OrdinalIgnoreCase));
            return new HexadecimalDigitLexer(innerLexer);
        }

        [NotNull]
        public HexadecimalDigitLexerFactory UseAlternationLexerFactory(
            [NotNull] IAlternationLexerFactory alternationLexerFactory)
        {
            if (alternationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alternationLexerFactory));
            }
            return new HexadecimalDigitLexerFactory(
                TerminalLexerFactory,
                alternationLexerFactory,
                DigitLexerFactory);
        }

        [NotNull]
        public HexadecimalDigitLexerFactory UseDigitLexerFactory([NotNull] ILexerFactory<Digit> digitLexerFactory)
        {
            if (digitLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(digitLexerFactory));
            }
            return new HexadecimalDigitLexerFactory(
                TerminalLexerFactory,
                AlternationLexerFactory,
                digitLexerFactory);
        }

        [NotNull]
        public HexadecimalDigitLexerFactory UseTerminalLexerFactory(
            [NotNull] ITerminalLexerFactory terminalLexerFactory)
        {
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }
            return new HexadecimalDigitLexerFactory(
                terminalLexerFactory,
                AlternationLexerFactory,
                DigitLexerFactory);
        }
    }
}
