using System;
using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF.Core.BIT
{
    /// <summary>Creates instances of the <see cref="BitLexer" /> class.</summary>
    public class BitLexerFactory : LexerFactory<Bit>
    {
        private ILexer<Bit> instance;

        static BitLexerFactory()
        {
            Default = new BitLexerFactory(
                ABNF.AlternationLexerFactory.Default,
                ABNF.TerminalLexerFactory.Default);
        }

        /// <summary>
        /// </summary>
        /// <param name="alternationLexerFactory"></param>
        /// <param name="terminalLexerFactory"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public BitLexerFactory(
            [NotNull] IAlternationLexerFactory alternationLexerFactory,
            [NotNull] ITerminalLexerFactory terminalLexerFactory)
        {
            if (alternationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alternationLexerFactory));
            }
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }
            AlternationLexerFactory = alternationLexerFactory;
            TerminalLexerFactory = terminalLexerFactory;
        }

        [NotNull]
        public static BitLexerFactory Default { get; }

        [NotNull]
        public IAlternationLexerFactory AlternationLexerFactory { get; }

        [NotNull]
        public ITerminalLexerFactory TerminalLexerFactory { get; }

        /// <inheritdoc />
        public override ILexer<Bit> Create()
        {
            var bit0TerminalLexer = TerminalLexerFactory.Create("0", StringComparer.Ordinal);
            var bit1TerminalLexer = TerminalLexerFactory.Create("1", StringComparer.Ordinal);
            var innerLexer = AlternationLexerFactory.Create(bit0TerminalLexer, bit1TerminalLexer);
            return new BitLexer(innerLexer);
        }

        [NotNull]
        public BitLexerFactory UseAlternationLexerFactory([NotNull] IAlternationLexerFactory alternationLexerFactory)
        {
            if (alternationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alternationLexerFactory));
            }
            return new BitLexerFactory(alternationLexerFactory, TerminalLexerFactory);
        }

        [NotNull]
        public BitLexerFactory UseTerminalLexerFactory([NotNull] ITerminalLexerFactory terminalLexerFactory)
        {
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }
            return new BitLexerFactory(AlternationLexerFactory, terminalLexerFactory);
        }
    }
}
